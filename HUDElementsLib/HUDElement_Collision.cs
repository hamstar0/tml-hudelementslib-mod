using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public virtual bool IgnoresCollisions() => false;

		public virtual bool IsAnchored() => false;


		////////////////

		public static Vector2 FindClosestNonCollidingPosition(
					Rectangle currentArea,
					Vector2 desiredPosition,
					string obstacleName,
					Rectangle obstacleArea ) {
			Vector2 pos = desiredPosition;

			if( currentArea.Bottom <= obstacleArea.Top ) {
				pos.Y = obstacleArea.Top - currentArea.Height;
			} else if( currentArea.Top >= obstacleArea.Bottom ) {
				pos.Y = obstacleArea.Bottom;
			} else if( currentArea.Right <= obstacleArea.Left ) {
				pos.X = obstacleArea.Left - (currentArea.Width + 2);
			} else if( currentArea.Left >= obstacleArea.Right ) {
				pos.X = obstacleArea.Right + 2;
			}

			return pos;
		}


		////////////////
		
		public static Vector2? FindDisplacedPositionIf( Rectangle currentArea, HUDElement element ) {
			Rectangle obstacleArea = element.GetRect();
			if( !currentArea.Intersects(obstacleArea) ) {
				return null;
			}

			Vector2 dir = element.GetDisplacementDirection() * 2f;

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
				if( testArea.Right <= 0 || testArea.Bottom <= 0 || testArea.Top >= Main.screenHeight || testArea.Left >= Main.screenWidth ) {
					return null;
				}
			}

			return new Vector2( testArea.X, testArea.Y );
		}
	}
}