using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private bool UpdateInteractionsIf( out bool isHovering ) {
			Rectangle area = this.GetAreaOnHUD( false );

			isHovering = area.Contains( Main.MouseScreen.ToPoint() );

			if( this.IsLocked() ) {
				return false;
			}

			return this.UpdateDragIf( isHovering );
		}
	}
}