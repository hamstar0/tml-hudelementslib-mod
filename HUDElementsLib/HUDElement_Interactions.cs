using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private void UpdateInteractionsIf( out bool isHovering ) {
			Rectangle area = this.GetAreaOnHUD( true );	// Original spot only

			isHovering = area.Contains( Main.MouseScreen.ToPoint() );

			if( this.IsLocked() ) {
				return;
			}

			this.UpdateControlsIf( isHovering );
			this.UpdateDragIf( isHovering );
		}


		////

		private void UpdateControlsIf( bool isHovering ) {
			if( !Main.mouseLeft ) {
				this.IsPressingControl = false;

				return;
			}
			if( this.IsPressingControl ) {
				return;
			}
			if( !isHovering ) {
				return;
			}
			if( this.IsDragging ) {
				return;
			}
			bool isAlt = Main.keyState.IsKeyDown( Keys.LeftAlt )
				|| Main.keyState.IsKeyDown( Keys.RightAlt );
			if( !isAlt ) {
				return;
			}

			Point mouse = Main.MouseScreen.ToPoint();
			Rectangle area = this.GetAreaOnHUD( true );

			Rectangle toggler = HUDElement.GetCollisionTogglerForBox( area );
			Rectangle anchorR = HUDElement.GetRightAnchorButtonForBox( area );
			Rectangle anchorB = HUDElement.GetBottomAnchorButtonForBox( area );

			if( toggler.Contains(mouse) ) {
				this.IsPressingControl = true;

Main.NewText("toggler "+this.Name);
				//todo
			} else if( anchorR.Contains(mouse) ) {
				this.IsPressingControl = true;
				
Main.NewText("ranchor "+this.Name);
				//todo
			} else if( anchorB.Contains(mouse) ) {
				this.IsPressingControl = true;
				
Main.NewText("banchor "+this.Name);
				//todo
			}
		}
	}
}