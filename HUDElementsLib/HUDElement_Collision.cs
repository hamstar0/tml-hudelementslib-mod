using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public static Vector2 FindClosestNonCollidingPosition(
					Rectangle currentArea,
					Vector2 desiredPosition,
					Rectangle obstacleArea ) {
			Vector2 pos = desiredPosition;

			if( currentArea.Bottom <= obstacleArea.Top ) {
				pos.Y = obstacleArea.Top - (currentArea.Height + 2);
			} else if( currentArea.Top >= obstacleArea.Bottom ) {
				pos.Y = obstacleArea.Bottom + 2;
			} else if( currentArea.Right <= obstacleArea.Left ) {
				pos.X = obstacleArea.Left - (currentArea.Width + 3);
			} else if( currentArea.Left >= obstacleArea.Right ) {
				pos.X = obstacleArea.Right + 3;
			}

			return pos;
		}


		////////////////
		
		public static Vector2? FindDisplacedPositionIf( Rectangle currentArea, HUDElement mover, HUDElement obstacle ) {
			Rectangle obstacleArea = obstacle.GetHUDComputedArea( true );
			if( !currentArea.Intersects(obstacleArea) ) {
				return null;
			}

			Vector2 dir = obstacle.GetDisplacementDirection( mover ) * 2f;

			//

			float fX = currentArea.X;
			float fY = currentArea.Y;

			void inc( ref Rectangle rect ) {
				fX += dir.X;
				fY += dir.Y;
				rect.X = (int)Math.Round( fX );
				rect.Y = (int)Math.Round( fY );
			}

			//

			Rectangle testArea;
			for( testArea = currentArea; testArea.Intersects(obstacleArea); inc(ref testArea) ) {	// Efficient!
				if( testArea.Right <= 0 ) {
					return null;
				}
				if( testArea.Bottom <= 0 ) {
					return null;
				}
				if( testArea.Top >= (Main.screenHeight-1) ) {
					return null;
				}
				if( testArea.Left >= (Main.screenWidth-1) ) {
					return null;
				}
			}

			return new Vector2( testArea.X, testArea.Y );
		}



		////////////////

		protected void ToggleCollisions() {
			if( !this.IsCollisionToggleable() ) {
				throw new Exception( "Invalid attempt to toggle collisions for " + this.Name );
			}
			this.IsIgnoringCollisions = !this.IsIgnoringCollisions;
		}
	}
}