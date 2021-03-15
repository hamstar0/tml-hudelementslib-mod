using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public sealed override void Draw( SpriteBatch sb ) {
			if( !this.IsEnabled() ) {
				return;
			}

			base.Draw( sb );

			this.DrawOverlaysIf( sb );
		}

		////

		private void DrawOverlaysIf( SpriteBatch sb ) {
			bool isHoverCollision = false;
			bool isHoverAnchorRight = false;
			bool isHoverAnchorBottom = false;

			if( Main.playerInventory ) {
				bool isAlt = Main.keyState.IsKeyDown( Keys.LeftAlt )
					|| Main.keyState.IsKeyDown( Keys.RightAlt );

				if( isAlt ) {
					this.DrawOverlayTitle( sb );
					this.DrawOverlayOfBoxes( sb );
					this.DrawOverlayOfControls( sb, out isHoverCollision, out isHoverAnchorRight, out isHoverAnchorBottom );
				}
			}

			string hoverText = this.GetHoverText( isHoverCollision, isHoverAnchorRight, isHoverAnchorBottom );
			if( !string.IsNullOrEmpty(hoverText) ) {
				this.DrawHoverTextIf( sb, hoverText );
			}
		}


		////////////////
		
		private void DrawOverlayTitle( SpriteBatch sb ) {
			Rectangle area = this.GetHUDComputedArea( false );

			sb.DrawString(
				spriteFont: Main.fontMouseText,
				text: this.Name,
				position: new Vector2( area.X+4, area.Y+8 ),
				color: Color.White,
				rotation: 0f,
				origin: default,
				scale: 0.75f,
				effects: SpriteEffects.None,
				layerDepth: 0
			);
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