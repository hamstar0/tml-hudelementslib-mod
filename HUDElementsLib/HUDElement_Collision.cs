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

		public Vector2 ResolveCollisionSmart( HUDElement element ) {
			int forWidth = (int)this.Width.Pixels;
			int forHeight = (int)this.Height.Pixels;

			//

			Rectangle againstArea = element.GetOuterDimensions().ToRectangle();

			againstArea.X -= forWidth / 2;
			if( againstArea.X < 0 ) {
				againstArea.X = 0;
			}

			againstArea.Width += forWidth;
			if( againstArea.Right >= Main.screenWidth ) {
				againstArea.Width = againstArea.Right - Main.screenWidth;
			}

			againstArea.Y -= forHeight / 2;
			if( againstArea.Y < 0 ) {
				againstArea.Y = 0;
			}

			againstArea.Height += forHeight;
			if( againstArea.Bottom >= Main.screenHeight ) {
				againstArea.Height = againstArea.Bottom - Main.screenHeight;
			}

			//

			int screenMidX = Main.screenWidth / 2;
			int screenMidY = Main.screenHeight / 2;

			bool isCloserToLeft = Math.Abs( screenMidX - againstArea.Left ) < Math.Abs( screenMidX - againstArea.Right );
			var pos = new Vector2( this.Left.Pixels, this.Top.Pixels );

			if( isCloserToLeft ) {
				pos.X = againstArea.Left - ( forWidth / 2 );
				if( pos.X < 0f ) {
					pos.X = 0f;
				}
			} else {
				pos.X = againstArea.Right - ( forWidth / 2 );

				float right = this.Width.Pixels + this.Left.Pixels;
				if( right >= Main.screenWidth ) {
					pos.X = right - Main.screenWidth;
				}
			}

			return pos;
		}


		public virtual Vector2 ResolveCollisionCustom( HUDElement element ) {
			return new Vector2( this.Left.Pixels, this.Top.Pixels );
		}
	}
}