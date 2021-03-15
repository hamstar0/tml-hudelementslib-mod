using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private void UpdateInteractionsForControlsIf( bool isAlt, bool mouseLeft ) {
			if( this.IsInteractingWithControls ) {
				bool isInteracting = mouseLeft && isAlt;	//&& this.IsMouseHovering_Custom;

				this.IsInteractingWithControls = isInteracting;
				Main.LocalPlayer.mouseInterface |= isInteracting;	// Repeatably locks control for this element, if needed

				return;	// Only the first tick of interaction matters
			}

			if( !isAlt ) { return; }
			if( !mouseLeft ) { return; }
			if( !this.IsMouseHovering_Custom ) { return; }
			if( this.IsDraggingSinceLastTick ) { return; }

			Point mouse = Main.MouseScreen.ToPoint();
			Rectangle area = this.GetHUDComputedArea( false );

			Rectangle toggler = HUDElement.GetCollisionTogglerForBox( area );
			Rectangle anchorR = HUDElement.GetRightAnchorButtonForBox( area );
			Rectangle anchorB = HUDElement.GetBottomAnchorButtonForBox( area );
			bool pressed = false;

			if( toggler.Contains(mouse) && this.CanToggleCollisionsViaControl() ) {
				pressed = this.ApplyCollisionsToggleControlPressIf();
			} else if( !this.IsAnchorLocked() ) {
				if( anchorR.Contains(mouse) ) {
					pressed = this.ApplyRightAnchorToggleControlPressIf();
				} else if( anchorB.Contains(mouse) ) {
					pressed = this.ApplyBottomAnchorToggleControlPressIf();
				}
			}

			this.IsInteractingWithControls = pressed;
			Main.LocalPlayer.mouseInterface |= pressed;
		}


		////////////////

		private void ResetInteractionsForControls() {
			this.IsInteractingWithControls = false;
		}


		////////////////

		private bool ApplyCollisionsToggleControlPressIf() {
			if( !HUDElementsLibConfig.Instance.EnableCollisionsToggleControl ) {
				return false;
			}

			this.IsInteractingWithControls = true;
			this.ToggleCollisions();

			return true;
		}

		private bool ApplyRightAnchorToggleControlPressIf() {
			if( !HUDElementsLibConfig.Instance.EnableAnchorsToggleControl ) {
				return false;
			}

			this.IsInteractingWithControls = true;
			this.ToggleRightAnchor();

			return true;
		}

		private bool ApplyBottomAnchorToggleControlPressIf() {
			if( !HUDElementsLibConfig.Instance.EnableAnchorsToggleControl ) {
				return false;
			}

			this.IsInteractingWithControls = true;
			this.ToggleBottomAnchor();

			return true;
		}
	}
}
