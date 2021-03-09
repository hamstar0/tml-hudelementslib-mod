using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private void UpdateInteractionsIf( out bool isHovering ) {
			Rectangle area = this.GetAreaOnHUD( true );	// Original spot only

			isHovering = area.Contains( Main.MouseScreen.ToPoint() );

			if( this.IsLocked() ) {
				return;
			}

			this.UpdateControlsIf( isHovering );
			this.UpdateDragIf( isHovering );
		}
	}
}