using Terraria;
using Terraria.GameInput;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		internal bool UpdateEditModeInteractionsWithinEntireUI() {
			//Rectangle area = this.GetHUDComputedArea( false );	<- Incorrect rectangle when game or ui zooms
			//this.IsMouseHovering_Custom = area.Contains( Main.MouseScreen.ToPoint() );

			//bool isInteracting = Main.playerInventory && Main.mouseLeft && this.IsMouseHovering_Custom;
			bool isInteracting = Main.mouseLeft
				&& this.IsMouseHoveringEditableBox
				&& HUDElementsLibAPI.IsEditModeActive();

			if( isInteracting ) {
				Main.LocalPlayer.mouseInterface = true; // Locks control for this element
			}

			return isInteracting;
		}


		////////////////

		private bool UpdateEditModeInteractions_If() {
			if( !this.IsEnabled() ) {
				return false;
			}

			bool mouseLeft = PlayerInput.Triggers.Current.MouseLeft;
			bool isEditMode = HUDElementsLibAPI.IsEditModeActive();

			this.UpdateInteractionsForEditModeControls_If( isEditMode, mouseLeft );
			this.UpdateInteractionsForEditModeDrag_If( isEditMode, mouseLeft );

			return true;
		}


		////

		private void ResetEditModeInteractions() {
			this.DesiredDragPosition = null;

			this.ResetInteractionsForControls();
		}
	}
}