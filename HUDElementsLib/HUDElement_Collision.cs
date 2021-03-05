using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public virtual bool AvoidsCollisions() => false;



		////////////////

		public bool CollidesWith( HUDElement element ) {
			int forX = (int)this.Left.Pixels;
			int forY = (int)this.Top.Pixels;
			int forWidth = (int)this.Width.Pixels;
			int forHeight = (int)this.Height.Pixels;
			int forMidX = forX + ( forWidth / 2 );
			int forMidY = forY + ( forHeight / 2 );

			Rectangle againstArea = element.GetRect();

			againstArea.X -= forWidth / 2;
			againstArea.Width += forWidth;

			againstArea.Y -= forHeight / 2;
			againstArea.Height += forHeight;

			return againstArea.Contains( forMidX, forMidY );
		}


		////////////////

		public virtual Vector2 ResolveCollision( Vector2 desiredPosition, Rectangle obstacleArea ) {
			int forWidth = (int)this.Width.Pixels;
			int forHeight = (int)this.Height.Pixels;

			//

			Rectangle paddedArea = obstacleArea;

			paddedArea.X -= forWidth / 2;
			if( paddedArea.X < 0 ) {
				paddedArea.X = 0;
			}

			paddedArea.Width += forWidth;
			if( paddedArea.Right >= Main.screenWidth ) {
				paddedArea.Width = paddedArea.Right - Main.screenWidth;
			}

			paddedArea.Y -= forHeight / 2;
			if( paddedArea.Y < 0 ) {
				paddedArea.Y = 0;
			}

			paddedArea.Height += forHeight;
			if( paddedArea.Bottom >= Main.screenHeight ) {
				paddedArea.Height = paddedArea.Bottom - Main.screenHeight;
			}

			//

			int screenMidX = Main.screenWidth / 2;
			int screenMidY = Main.screenHeight / 2;

			bool isCloserToLeft = Math.Abs( screenMidX - paddedArea.Left ) < Math.Abs( screenMidX - paddedArea.Right );
			Vector2 pos = desiredPosition;

			if( isCloserToLeft ) {
				pos.X = paddedArea.Left - ( forWidth / 2 );
				if( pos.X < 0f ) {
					pos.X = 0f;
				}
			} else {
				pos.X = paddedArea.Right - ( forWidth / 2 );

				float right = this.Width.Pixels + this.Left.Pixels;
				if( right >= Main.screenWidth ) {
					pos.X = right - Main.screenWidth;
				}
			}

			return pos;
		}
	}
}