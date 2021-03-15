using Microsoft.Xna.Framework.Input;
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
			//bool editMode = Main.keyState.IsKeyDown( Keys.LeftAlt )
			//	|| Main.keyState.IsKeyDown( Keys.RightAlt );
			bool editMode = HUDElementsLibMod.Instance.HUDEditMode.Current;

			this.UpdateInteractionsForControlsIf( editMode, mouseLeft );
			this.UpdateInteractionsForDragIf( editMode, mouseLeft );

			return true;
		}


		////

		private void ResetInteractions() {
			this.DesiredDragPosition = null;

			this.ResetInteractionsForControls();
		}
	}
}