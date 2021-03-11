using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private void UpdateControlsIf( bool isHovering ) {
			if( !Main.mouseLeft ) {
				this.IsPressingControl = false;

				return;
			}
			if( this.IsPressingControl ) {
				Main.LocalPlayer.mouseInterface = true;	// Locks control for this element

				return;
			}
			if( !isHovering ) {
				return;
			}
			if( this.IsDragging ) {
				return;
			}
			bool isAlt = Main.keyState.IsKeyDown( Keys.LeftAlt )
				|| Main.keyState.IsKeyDown( Keys.RightAlt );
			if( !isAlt ) {
				return;
			}

			Point mouse = Main.MouseScreen.ToPoint();
			Rectangle area = this.GetHUDComputedArea( false );

			Rectangle toggler = HUDElement.GetCollisionTogglerForBox( area );
			Rectangle anchorR = HUDElement.GetRightAnchorButtonForBox( area );
			Rectangle anchorB = HUDElement.GetBottomAnchorButtonForBox( area );

			if( toggler.Contains(mouse) && this.CanToggleCollisionsViaControl() ) {
				this.IsPressingControl = true;
				this.ToggleCollisions();
			} else if( !this.IsLocked() ) {
				if( anchorR.Contains(mouse) ) {
					this.IsPressingControl = true;
					this.ToggleRightAnchor();
				} else if( anchorB.Contains(mouse) ) {
					this.IsPressingControl = true;
					this.ToggleBottomAnchor();
				}
			}

			if( this.IsPressingControl ) {
				Main.LocalPlayer.mouseInterface = true;
			}
		}
	}
}
