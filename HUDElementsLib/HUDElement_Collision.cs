using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public enum HUDCollision {
		None,
		Smart,
		Custom
	}




	public partial class HUDElement : UIElement {
		public virtual HUDCollision CollisionDecision() {
			return HUDCollision.Smart;
		}


		////////////////

		public bool CollidesWith( HUDElement element ) {
			int forX = (int)this.Left.Pixels;
			int forY = (int)this.Top.Pixels;
			int forWidth = (int)this.Width.Pixels;
			int forHeight = (int)this.Height.Pixels;
			int forMidX = forX + ( forWidth / 2 );
			int forMidY = forY + ( forHeight / 2 );

			Rectangle againstArea = element.GetOuterDimensions().ToRectangle();

			againstArea.X -= forWidth / 2;
			againstArea.Width += forWidth;

			againstArea.Y -= forHeight / 2;
			againstArea.Height += forHeight;

			return againstArea.Contains( forMidX, forMidY );
		}


		////////////////

		public Vector2 ResolveCollisionSmart( Rectangle obstacleArea ) {
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
			var pos = new Vector2( this.Left.Pixels, this.Top.Pixels );

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


		public virtual Vector2 ResolveCollisionCustom( Rectangle obstacleArea ) {
			return new Vector2( this.Left.Pixels, this.Top.Pixels );
		}
	}
}