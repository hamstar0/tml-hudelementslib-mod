using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.UI;
using Terraria.GameInput;
using Terraria.ModLoader;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private bool UpdateDrag( bool isHovering ) {
			HUDElement currDrag = HUDElementsLibAPI.GetDraggingElement();
			if( currDrag != null && currDrag != this ) {
				return false;
			}

			bool mouseLeft = PlayerInput.Triggers.Current.MouseLeft;
			bool isAlt = Main.keyState.IsKeyDown( Keys.LeftAlt )
				|| Main.keyState.IsKeyDown( Keys.RightAlt );

			if( mouseLeft && isAlt ) {
				if( this.IsDragging || isHovering ) {
					this.ApplyDrag();
				}
			} else {
				this.DesiredDragPosition = null;
			}

			return this.DesiredDragPosition.HasValue;
		}


		////////////////

		private void ApplyDrag() {
			Main.LocalPlayer.mouseInterface = true;

			if( !this.DesiredDragPosition.HasValue ) {
				this.DesiredDragPosition = this.GetPositionOnHUD( true );
				this.PreviousDragMousePos = Main.MouseScreen;

				return;
			}

			var mymod = ModContent.GetInstance<HUDElementsLibMod>();

			Vector2 movedSince = Main.MouseScreen - this.PreviousDragMousePos;
			this.PreviousDragMousePos = Main.MouseScreen;

			if( movedSince == default ) {
				return;
			}

			this.DesiredDragPosition += movedSince;

			Vector2 validPos = mymod.HUDManager.FindNonCollidingPosition( this, this.DesiredDragPosition.Value );

			this.SetBasePosition( validPos );
		}
	}
}