using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public virtual bool IsDragLocked() {
			return false;
		}
		

		////////////////

		private void UpdateInteractionsForEditModeDrag_If( bool isEditMode, bool mouseLeft ) {
			if( this.IsInteractingWithControls ) { return; }
			if( this.IsDragLocked() ) { return; }

			//

			HUDElement currDrag = HUDElementsLibAPI.GetDraggingElement();

			if( currDrag == null || currDrag == this ) {
				bool isInteracting = mouseLeft && isEditMode;

				if( isInteracting ) {
					if( this.IsDraggingSinceLastTick || this.IsMouseHoveringEditableBox ) {
						this.ApplyDrag();
					}
				} else {
					this.DesiredDragPosition = null;
				}
			}
		}


		////////////////

		private void ApplyDrag() {
			Main.LocalPlayer.mouseInterface = true;

			// Initialize drag state
			if( !this.IsDraggingSinceLastTick ) {
				this.DesiredDragPosition = this.GetHUDComputedPosition( false );
				this.PreviousDragMousePos = Main.MouseScreen;

				return;
			}

			//

			var mymod = ModContent.GetInstance<HUDElementsLibMod>();

			Vector2 movedSince = Main.MouseScreen - this.PreviousDragMousePos;
			this.PreviousDragMousePos = Main.MouseScreen;

			if( movedSince == default ) {
				return;
			}

			//

			this.DesiredDragPosition += movedSince;

			if( this.DesiredDragPosition.Value.X <= 0 ) {
				this.DesiredDragPosition = new Vector2( 1, this.DesiredDragPosition.Value.Y );
			} else if( this.DesiredDragPosition.Value.X >= (Main.screenWidth - 1) ) {
				this.DesiredDragPosition = new Vector2( (Main.screenWidth - 2), this.DesiredDragPosition.Value.Y );
			}
			if( this.DesiredDragPosition.Value.Y <= 0 ) {
				this.DesiredDragPosition = new Vector2( this.DesiredDragPosition.Value.X, 1 );
			} else if( this.DesiredDragPosition.Value.Y >= (Main.screenHeight - 1) ) {
				this.DesiredDragPosition = new Vector2( this.DesiredDragPosition.Value.X, (Main.screenHeight - 2) );
			}

			Vector2 validPos = mymod.HUDManager.FindNonCollidingPosition( this, this.DesiredDragPosition.Value );
			//Vector2 validPos = this.DesiredDragPosition.Value;

			this.SetIntendedPosition( validPos, true );
		}
	}
}