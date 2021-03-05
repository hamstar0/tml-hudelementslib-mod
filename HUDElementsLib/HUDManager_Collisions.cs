using System;
using Microsoft.Xna.Framework;
using Terraria;


namespace HUDElementsLib {
	partial class HUDManager {
		public Vector2 HandleCollisions( HUDElement element, Vector2 oldPosition, Vector2 desiredPosition ) {
			if( element.IgnoresCollisions() ) {
				return desiredPosition;
			}

			Vector2 bestPosition = desiredPosition;

			for( int i = 0; i < 10; i++ ) { // <- lazy
				Vector2 testPos = this.HandleFirstFoundCollision( element, oldPosition, bestPosition );
				if( testPos == bestPosition ) {
					return bestPosition;
				}

				bestPosition = testPos;
			}

			return oldPosition;
		}


		private Vector2 HandleFirstFoundCollision( HUDElement element, Vector2 oldPosition, Vector2 desiredPosition ) {
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

				return element.ResolveCollision(
					oldPosition,
					desiredPosition,
					elemName,
					obstacleArea
				);
			}

			return desiredPosition;
		}
	}
}