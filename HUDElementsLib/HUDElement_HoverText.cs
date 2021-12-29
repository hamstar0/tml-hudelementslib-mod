using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public virtual (string text, int duration) GetHoverText(
					bool editMode,
					bool isCollisionToggleButton,
					bool isResetButton,
					bool isAnchorRightToggle,
					bool isAnchorBottomToggle ) {
			if( !this.IsMouseHoveringEditableBox ) {
				return ("", -1);
			}

			if( editMode ) {
				if( this.IsCollisionToggleable() ) {
					if( isCollisionToggleButton ) {
						return ("Toggle collisions", -1);
					}
				}
				
				if( !this.IsDragLocked() ) {
					if( isResetButton ) {
						return ("Reset position", -1);
					}
				}

				if( this.IsAnchorsToggleable() ) {
					if( isAnchorRightToggle ) {
						return ("Anchor to right edge of screen", -1);
					}
					if( isAnchorBottomToggle ) {
						return ("Anchor to bottom edge of screen", -1);
					}
				}

				if( !this.IsDragLocked() ) {
					return ("Alt+Click to drag", -1);
				}
			} else {
				if( Main.playerInventory && !this.IsDragLocked() ) {
					if( HUDElementsLibMod.Instance.HUDEditMode.GetAssignedKeys().Count == 0 ) {
						return ("Bind 'Edit Mode' to a key to interact", 60);
					}
				}
			}

			return ("", -1);
		}
	}
}