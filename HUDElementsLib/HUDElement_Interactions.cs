using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		internal bool PreUpdateForInteractions() {
			if( !Main.mouseLeft ) {
				return false;
			}

			Rectangle area = this.GetHUDComputedArea( false );

			if( !area.Contains(Main.MouseScreen.ToPoint()) ) {
				return false;
			}

			Main.LocalPlayer.mouseInterface = true; // Locks control for this element

			return true;
		}


		////

		private void UpdateInteractionsIf( out bool isHovering ) {
			Rectangle area = this.GetHUDComputedArea( false );	// Original spot only

			isHovering = area.Contains( Main.MouseScreen.ToPoint() );

			this.UpdateControlsIf( isHovering );
			this.UpdateDragIf( isHovering );
		}
	}
}