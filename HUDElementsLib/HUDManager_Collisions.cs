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
			Rectangle desiredArea = element.GetRect();
			desiredArea.X = (int)desiredPosition.X - 1;
			desiredArea.Y = (int)desiredPosition.Y - 1;
			desiredArea.Width += 2;
			desiredArea.Height += 2;

			foreach( string elemName in this.Elements.Keys ) {
				HUDElement elem = this.Elements[ elemName ];

				if( elem == element ) {
					continue;
				}

				Rectangle obstacleArea = elem.GetRect();
				if( !desiredArea.Intersects(obstacleArea) ) {
					continue;
				}

				return HUDElement.FindClosestNonCollidingPosition(
					currentArea: element.GetRect(),
					desiredPosition: desiredPosition,
					obstacleName: elemName,
					obstacleArea: obstacleArea
				);
			}

			return desiredPosition;
		}
	}
}