using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private void UpdateInteractions() {
			bool mouseLeft = PlayerInput.Triggers.Current.MouseLeft;
			bool isAlt = Main.keyState.IsKeyDown( Keys.LeftAlt )
				|| Main.keyState.IsKeyDown( Keys.RightAlt );

			this.UpdateInteractionsForControlsIf( isAlt, mouseLeft );
			this.UpdateInteractionsForDragIf( isAlt, mouseLeft );
		}


		private void ResetInteractions() {
			this.DesiredDragPosition = null;

			this.ResetInteractionsForControls();
		}
	}
}