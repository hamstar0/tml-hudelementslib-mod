using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
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

		public virtual bool IgnoresCollisions() => false;
	}
}