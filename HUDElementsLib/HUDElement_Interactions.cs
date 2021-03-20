using Terraria;
using Terraria.GameInput;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private bool UpdateInteractionsIf() {
			if( !this.IsEnabled() ) {
				return false;
			}

			bool mouseLeft = PlayerInput.Triggers.Current.MouseLeft;
			bool isEditMode = HUDElementsLibAPI.IsEditModeActive();

			this.UpdateInteractionsForControlsIf( isEditMode, mouseLeft );
			this.UpdateInteractionsForDragIf( isEditMode, mouseLeft );

			return true;
		}


		////

		private void ResetInteractions() {
			this.DesiredDragPosition = null;

			this.ResetInteractionsForControls();
		}
	}
}