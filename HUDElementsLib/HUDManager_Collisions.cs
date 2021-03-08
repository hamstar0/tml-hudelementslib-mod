using System;
using Microsoft.Xna.Framework;
using Terraria;


namespace HUDElementsLib {
	partial class HUDManager {
		public Vector2 FindNonCollidingPosition( HUDElement element, Vector2 desiredPosition ) {
			if( element.IsIgnoringCollisions() ) {
				return desiredPosition;
			}

			Vector2 ogPosition = element.GetPositionOnHUD( true );
			Vector2 bestPosition = desiredPosition;

			for( int i = 0; i < 10; i++ ) { // <- lazy
				Vector2? testPos = this.FindFirstCollisionSolvedPosition( element, bestPosition );
				if( !testPos.HasValue ) {
					return bestPosition;
				}

				if( bestPosition == desiredPosition ) {
					bestPosition = testPos.Value;
				} else {
					break;	// <- even more lazy
				}
			}

			return ogPosition;
		}


		private Vector2? FindFirstCollisionSolvedPosition( HUDElement element, Vector2 desiredPosition ) {
			Rectangle currentArea = element.GetAreaOnHUD( true );
			Rectangle desiredArea = currentArea;
			desiredArea.X = (int)desiredPosition.X - 1;
			desiredArea.Y = (int)desiredPosition.Y - 1;
			desiredArea.Width += 2;
			desiredArea.Height += 2;

			foreach( string elemName in this.Elements.Keys ) {
				HUDElement elem = this.Elements[ elemName ];
				if( elem == element ) {
					continue;
				}
				if( !elem.IsEnabled() ) {
					continue;
				}

				Rectangle obstacleArea = elem.GetAreaOnHUD( false );

				if( desiredArea.Intersects(obstacleArea) ) {
					return HUDElement.FindClosestNonCollidingPosition(
						currentArea: currentArea,
						desiredPosition: desiredPosition,
						obstacleName: elemName,
						obstacleArea: obstacleArea
					);
				}
			}

			return null;
		}


		////////////////

		public bool ApplyDisplacementsIf( HUDElement element ) {
			if( element.IsLocked() ) {
				return false;
			}

			Rectangle currentArea = element.GetAreaOnHUD( true );
			bool isDisplaced = false;

			for( int i=0; i<10; i++ ) {
				bool isNowDisplaced = false;

				foreach( string elemName in this.Elements.Keys ) {
					HUDElement elem = this.Elements[elemName];
					if( elem == element ) {
						continue;
					}
					if( !elem.IsEnabled() ) {
						continue;
					}
					if( elem.IsIgnoringCollisions() ) {
						continue;
					}

					Vector2? displacedPos = HUDElement.FindDisplacedPositionIf( currentArea, elem );
					if( !displacedPos.HasValue ) {
						continue;
					}

					element.SetDisplacedPosition( displacedPos.Value );
					currentArea = element.GetAreaOnHUD( false );

					isNowDisplaced = true;
					isDisplaced = true;
					break;
				}

				if( !isNowDisplaced ) {
					break;
				}
			}

			if( !isDisplaced ) {
				element.RevertDisplacedPosition();
			}
//if( element.Name == "PKE Meter" || element.Name == "PKEMeter" ) {
//ModContent.GetInstance<HUDElementsLibMod>().Logger.Info( element.Name+" displaced? "+isDisplaced
//	+" basepos: "+element.GetPositionOnHUD(true)
//	+" displacepos: "+element.GetPositionOnHUD(false) );
//}

			return isDisplaced;
		}
	}
}