using Microsoft.Xna.Framework.Graphics;
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
			bool isHoverReset = false;

			//if( Main.playerInventory ) {
			//bool mode = Main.keyState.IsKeyDown( Keys.LeftAlt )
			//	|| Main.keyState.IsKeyDown( Keys.RightAlt );
			bool editMode = HUDElementsLibAPI.IsEditModeActive();

			if( editMode ) {
				this.DrawOverlayTitle( sb );
				this.DrawOverlayOfBoxes( sb );
				this.DrawOverlayOfControls(
					sb,
					out isHoverCollision,
					out isHoverReset,
					out isHoverAnchorRight,
					out isHoverAnchorBottom
				);
			}

			(string text, int duration) hoverInfo = this.GetHoverText(
				editMode,
				isHoverCollision,
				isHoverReset,
				isHoverAnchorRight,
				isHoverAnchorBottom
			);

			if( !string.IsNullOrEmpty(hoverInfo.text ) ) {
				this.DrawHoverTextIf( sb, hoverInfo.text, hoverInfo.duration );
			} else {
				this.ClearHoverText();
			}
		}
	}
}