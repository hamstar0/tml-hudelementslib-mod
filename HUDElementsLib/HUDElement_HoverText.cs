using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public virtual (string text, int duration) GetHoverText(
					bool editMode,
					bool isCollisionToggleButton,
					bool isAnchorRightToggle,
					bool isAnchorBottomToggle ) {
			if( !this.IsMouseHovering_Custom ) {
				return ("", -1);
			}

			if( editMode ) {
				if( this.IsCollisionToggleable() ) {
					if( isCollisionToggleButton ) {
						return ("Toggle collisions", -1);
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
					return ("Bind 'Edit Mode' to a key to interact", 45);
				}
			}

			return ("", -1);
		}
	}
}