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

		private void UpdateInteractionsForEditModeControls_If( bool isEditMode, bool mouseLeft ) {
			if( this.IsInteractingWithControls ) {
				bool isInteracting = mouseLeft && isEditMode;	//&& this.IsMouseHovering_Custom;

				this.IsInteractingWithControls = isInteracting;
				Main.LocalPlayer.mouseInterface |= isInteracting;	// Repeatably locks control for this element, if needed
//if( Main.LocalPlayer.mouseInterface ) {
//	Main.NewText( "HUD_UpdIntForCtrlIf 1" );
//}

				return;	// Only the first tick of interaction matters
			}

			//

			if( !isEditMode ) { return; }
			if( !mouseLeft ) { return; }
			if( !this.IsMouseHoveringEditableBox ) { return; }
			if( HUDElementsLibAPI.GetDraggingElement() != null ) { return; }

			//

			Point mouse = Main.MouseScreen.ToPoint();
			Rectangle area = this.GetHUDComputedArea( false );

			Rectangle toggler = HUDElement.GetCollisionTogglerArea( area );
			Rectangle reset = HUDElement.GetResetButtonArea( area );
			Rectangle anchorR = HUDElement.GetRightAnchorButtonArea( area );
			Rectangle anchorB = HUDElement.GetBottomAnchorButtonArea( area );
			bool pressed = false;

			if( toggler.Contains(mouse) && this.IsCollisionToggleable() ) {
				pressed = this.ApplyCollisionsToggleControlPress_If();
			} else if( reset.Contains(mouse) && !this.IsDragLocked() ) {
				pressed = true;
				this.ResetPositionToDefault();
			} else if( this.IsAnchorsToggleable() ) {
				if( anchorR.Contains(mouse) ) {
					pressed = this.ApplyRightAnchorToggleControlPress_If();
				} else if( anchorB.Contains(mouse) ) {
					pressed = this.ApplyBottomAnchorToggleControlPress_If();
				}
			}

			this.IsInteractingWithControls = pressed;
			Main.LocalPlayer.mouseInterface |= pressed;
//if( Main.LocalPlayer.mouseInterface ) {
//	Main.NewText( "HUD_UpdIntForCtrlIf 2" );
//}
		}


		////////////////

		private void ResetInteractionsForControls() {
			this.IsInteractingWithControls = false;
		}


		////

		private bool ApplyCollisionsToggleControlPress_If() {
			if( !HUDElementsLibConfig.Instance.EnableCollisionsToggleControl ) {
				return false;
			}

			this.IsInteractingWithControls = true;
			this.ToggleCollisions();

			return true;
		}

		private bool ApplyRightAnchorToggleControlPress_If() {
			if( !HUDElementsLibConfig.Instance.EnableAnchorsToggleControl ) {
				return false;
			}

			this.IsInteractingWithControls = true;
			this.ToggleRightAnchor();

			return true;
		}

		private bool ApplyBottomAnchorToggleControlPress_If() {
			if( !HUDElementsLibConfig.Instance.EnableAnchorsToggleControl ) {
				return false;
			}

			this.IsInteractingWithControls = true;
			this.ToggleBottomAnchor();

			return true;
		}
	}
}
