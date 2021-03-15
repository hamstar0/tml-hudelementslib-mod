using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public override void Draw( SpriteBatch sb ) {
			if( !this.IsEnabled() ) {
				return;
			}

			base.Draw( sb );

			this.DrawOverlaysIf( sb );
		}

		private void DrawOverlaysIf( SpriteBatch sb ) {
			bool isHoverCollision = false;
			bool isHoverAnchorRight = false;
			bool isHoverAnchorBottom = false;

			if( Main.playerInventory ) {
				bool isAlt = Main.keyState.IsKeyDown( Keys.LeftAlt )
					|| Main.keyState.IsKeyDown( Keys.RightAlt );

				if( isAlt ) {
					this.DrawOverlayBoxes( sb );
					this.DrawOverlayControls( sb, out isHoverCollision, out isHoverAnchorRight, out isHoverAnchorBottom );
				}
			}

			string hoverText = this.GetHoverText( isHoverCollision, isHoverAnchorRight, isHoverAnchorBottom );
			if( !string.IsNullOrEmpty(hoverText) ) {
				this.DrawHoverTextIf( sb, hoverText );
			}
		}


		////////////////

		private void DrawHoverTextIf( SpriteBatch sb, string text ) {
			if( !this.IsMouseHovering_Custom ) {
				return;
			}
			if( this.IsInteractingAny ) {
				return;
			}

			Utils.DrawBorderStringFourWay(
				sb: sb,
				font: Main.fontMouseText,
				text: text,
				x: Main.MouseScreen.X + 12,
				y: Main.MouseScreen.Y + 16,
				textColor: new Color( Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor ),
				borderColor: Color.Black,
				origin: default
			);
		}
	}
}