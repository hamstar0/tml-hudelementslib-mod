using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.UI;
using Terraria.GameInput;
using Terraria.ModLoader;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private bool RunHUDEditorIf( out bool isHovering ) {
			Rectangle area = this.GetRect();

			isHovering = area.Contains( Main.MouseScreen.ToPoint() );

			HUDElement currDrag = HUDElementsLibAPI.GetDraggingElement();
			if( currDrag != null && currDrag != this ) {
				return false;
			}

			bool mouseLeft = PlayerInput.Triggers.Current.MouseLeft;
			bool isAlt = Main.keyState.IsKeyDown( Keys.LeftAlt )
				|| Main.keyState.IsKeyDown( Keys.RightAlt );

			if( mouseLeft && isAlt ) {
				if( this.IsDragging || isHovering ) {
					this.RunHUDEditor_Drag();
				}
			} else {
				this.DesiredDragPosition = null;
			}

			return this.DesiredDragPosition.HasValue;
		}


		private void RunHUDEditor_Drag() {
			Main.LocalPlayer.mouseInterface = true;

			if( !this.DesiredDragPosition.HasValue ) {
				this.DesiredDragPosition = new Vector2( this.Left.Pixels, this.Top.Pixels );
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

			Vector2? validatedNewPosition = mymod.HUDManager.HandleCollisions( this, this.DesiredDragPosition.Value );

			if( validatedNewPosition.HasValue ) {
				this.Left.Pixels = validatedNewPosition.Value.X;
				this.Top.Pixels = validatedNewPosition.Value.Y;
			}
		}
	}
}