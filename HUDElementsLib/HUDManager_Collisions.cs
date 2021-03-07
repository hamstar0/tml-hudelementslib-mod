using System;
using Microsoft.Xna.Framework;
using Terraria;


namespace HUDElementsLib {
	partial class HUDManager {
		public Vector2 FindNonCollidingPosition( HUDElement element, Vector2 desiredPosition ) {
			if( element.IgnoresCollisions() ) {
				return desiredPosition;
			}

			Vector2 bestPosition = desiredPosition;

			for( int i = 0; i < 10; i++ ) { // <- lazy
				Vector2 testPos = this.FindFirstCollisionSolvedPosition( element, bestPosition );
				if( testPos == bestPosition ) {
					return bestPosition;
				}

				bestPosition = testPos;
			}

			return new Vector2( element.Left.Pixels, element.Top.Pixels );
		}


		private Vector2 FindFirstCollisionSolvedPosition( HUDElement element, Vector2 desiredPosition ) {
			Rectangle currentArea = element.GetRect();
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

				Rectangle obstacleArea = elem.GetRect();

				if( desiredArea.Intersects(obstacleArea) ) {
					return HUDElement.FindClosestNonCollidingPosition(
						currentArea: currentArea,
						desiredPosition: desiredPosition,
						obstacleName: elemName,
						obstacleArea: obstacleArea
					);
				}
			}

			return desiredPosition;
		}


		////

		public bool ApplyDisplacementsIf( HUDElement element ) {
			bool isDisplaced = false;
			Rectangle currentArea = element.GetRect( true );

			foreach( string elemName in this.Elements.Keys ) {
				HUDElement elem = this.Elements[elemName];
				if( elem == element ) {
					continue;
				}

				Vector2? displacedPos = HUDElement.FindDisplacedPositionIf( currentArea, elem );
				if( !displacedPos.HasValue ) {
					continue;
				}

				element.SetDisplacedPosition( displacedPos.Value );
				currentArea = element.GetRect( false );

				isDisplaced = true;
			}

			if( !isDisplaced ) {
				element.RevertDisplacedPosition();
			}

			return isDisplaced;
		}
	}
}