using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public virtual bool IgnoresCollisions() => false;



		////////////////

		public virtual Vector2 ResolveCollision( Vector2 oldPosition, Vector2 desiredPosition, string obstacleName, Rectangle obstacle ) {
			return oldPosition;
			/*Rectangle rect = this.GetRect();
			Rectangle obstaclePadding = obstacle;

			obstaclePadding.X -= rect.Width / 2;
			if( obstaclePadding.X < 0 ) {
				obstaclePadding.X = 0;
			}

			obstaclePadding.Width += rect.Width;
			if( obstaclePadding.Right >= Main.screenWidth ) {
				obstaclePadding.Width = obstaclePadding.Right - Main.screenWidth;
			}

			obstaclePadding.Y -= rect.Height / 2;
			if( obstaclePadding.Y < 0 ) {
				obstaclePadding.Y = 0;
			}

			obstaclePadding.Height += rect.Height;
			if( obstaclePadding.Bottom >= Main.screenHeight ) {
				obstaclePadding.Height = obstaclePadding.Bottom - Main.screenHeight;
			}

			//

			Vector2 pos = desiredPosition;
			int midDistX = Math.Abs( (int)pos.X - obstacle.Center.X );
			int midDistY = Math.Abs( (int)pos.Y - obstacle.Center.Y );

			if( midDistX < midDistY ) {
				if( pos.X < obstacle.Center.X ) {
					pos.X = obstacle.Left - rect.Width - 1;
				} else {
					pos.X = obstacle.Right + 1;
				}
			} else {
				if( pos.Y < obstacle.Center.Y ) {
					pos.Y = obstacle.Top - rect.Height - 1;
				} else {
					pos.Y = obstacle.Bottom + 1;
				}
			}

			return pos;*/
		}
	}
}