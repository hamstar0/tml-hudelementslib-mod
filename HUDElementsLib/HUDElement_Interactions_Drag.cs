using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.UI;
using Terraria.GameInput;
using Terraria.ModLoader;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private bool UpdateDragIf( bool isHovering ) {
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
			if( this.DesiredDragPosition.Value.X <= 0 ) {
				this.DesiredDragPosition = new Vector2( 1, this.DesiredDragPosition.Value.Y );
			} else if( this.DesiredDragPosition.Value.X >= (Main.screenWidth - 1) ) {
				this.DesiredDragPosition = new Vector2( (Main.screenWidth - 2), this.DesiredDragPosition.Value.Y );
			}
			if( this.DesiredDragPosition.Value.Y <= 0 ) {
				this.DesiredDragPosition = new Vector2( this.DesiredDragPosition.Value.Y, 1 );
			} else if( this.DesiredDragPosition.Value.Y >= (Main.screenHeight - 1) ) {
				this.DesiredDragPosition = new Vector2( this.DesiredDragPosition.Value.X, (Main.screenHeight - 2) );
			}

			Vector2 validPos = mymod.HUDManager.FindNonCollidingPosition( this, this.DesiredDragPosition.Value );

			this.SetBasePosition( validPos );
		}
	}
}