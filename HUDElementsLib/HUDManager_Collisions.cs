using System;
using Microsoft.Xna.Framework;
using Terraria;


namespace HUDElementsLib {
	partial class HUDManager {
		public Vector2 FindNonCollidingPosition( HUDElement element, Vector2 desiredPosition ) {
			if( element.IsIgnoringCollisions ) {
				return desiredPosition;
			}

			Vector2 ogPosition = element.GetHUDComputedPosition( false );
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
			Rectangle currentArea = element.GetHUDComputedArea( false );
			Rectangle desiredArea = currentArea;
			desiredArea.X = (int)desiredPosition.X - 1;
			desiredArea.Y = (int)desiredPosition.Y - 1;
			desiredArea.Width += 2;
			desiredArea.Height += 2;

			foreach( HUDElement elem in this.Elements.Values ) {
				if( elem == element ) { continue; }
				if( !elem.IsEnabled() ) { continue; }
				if( elem.IsIgnoringCollisions ) { continue; }

				Rectangle obstacleArea = elem.GetHUDComputedArea( true );

				if( desiredArea.Intersects(obstacleArea) ) {
					return HUDElement.FindClosestNonCollidingPosition(
						currentArea: currentArea,
						desiredPosition: desiredPosition,
						obstacleArea: obstacleArea
					);
				}
			}

			return null;
		}


		////////////////

		public HUDElement FindFirstCollision( HUDElement element ) {
			if( element.IsDragLocked() ) { return null; }
			if( element.IsIgnoringCollisions ) { return null; }

			Rectangle currentArea = element.GetHUDComputedArea( false );

			foreach( HUDElement obstacle in this.Elements.Values ) {
				if( obstacle == element ) { continue; }
				if( !obstacle.IsEnabled() ) { continue; }
				if( obstacle.IsIgnoringCollisions ) { continue; }

				Rectangle obstacleArea = obstacle.GetHUDComputedArea( true );
				if( currentArea.Intersects(obstacleArea) ) {
					return obstacle;
				}
			}

			return null;
		}


		////////////////

		public void FindAndApplyDisplacements( HUDElement element ) {
			if( !this.FindAndApplyDisplacements_If(element) ) {
				element.RevertDisplacedOffset();
			}
		}

		private bool FindAndApplyDisplacements_If( HUDElement element ) {
			if( element.IsIgnoringCollisions ) { return false; }

			//

			bool isDisplaced = false;

			Rectangle currentArea = element.GetHUDComputedArea( false );

			for( int i=0; i<250; i++ ) {
				HUDElement obstacle = this.FindFirstCollision( element );
				if( obstacle == null ) {
					break;
				}

				Vector2? displacedPosition = HUDElement.FindDisplacedPosition_If( currentArea, element, obstacle );
				if( !displacedPosition.HasValue ) {
					break;
				}

				//

				Vector2 displacedOffset = displacedPosition.Value - element.GetHUDComputedPosition( false );

				isDisplaced = true;
				element.SetDisplacedOffset( displacedOffset );
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