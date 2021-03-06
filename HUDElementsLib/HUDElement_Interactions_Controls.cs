using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public virtual bool IsCollisionToggleable() {
			return true;
		}
		
		public virtual bool IsAnchorsToggleable() {
			return !this.AutoAnchors();
		}


		////////////////

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
			if( HUDElementsLibAPI.GetDraggingElement() != null ) { return; }

			Point mouse = Main.MouseScreen.ToPoint();
			Rectangle area = this.GetHUDComputedArea( false );

			Rectangle toggler = HUDElement.GetCollisionTogglerArea( area );
			Rectangle reset = HUDElement.GetResetButtonArea( area );
			Rectangle anchorR = HUDElement.GetRightAnchorButtonArea( area );
			Rectangle anchorB = HUDElement.GetBottomAnchorButtonArea( area );
			bool pressed = false;

			if( toggler.Contains(mouse) && this.IsCollisionToggleable() ) {
				pressed = this.ApplyCollisionsToggleControlPressIf();
			} else if( reset.Contains(mouse) && !this.IsDragLocked() ) {
				pressed = true;
				this.ResetPositionToDefault();
			} else if( this.IsAnchorsToggleable() ) {
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


		////

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
