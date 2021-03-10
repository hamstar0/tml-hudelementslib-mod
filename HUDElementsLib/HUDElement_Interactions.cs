using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private void UpdateInteractionsIf( out bool isHovering ) {
			Rectangle area = this.GetHudComputedArea( true );	// Original spot only

			isHovering = area.Contains( Main.MouseScreen.ToPoint() );

			this.UpdateControlsIf( isHovering );
			this.UpdateDragIf( isHovering );
		}
	}
}