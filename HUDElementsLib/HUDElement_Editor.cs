using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private bool RunHUDEditorIf( out bool isHovering ) {
			var pos = new Vector2( this.Left.Pixels, this.Top.Pixels );
			var area = new Rectangle(
				(int)pos.X,
				(int)pos.Y,
				(int)this.Width.Pixels,
				(int)this.Height.Pixels
			);

			isHovering = area.Contains( Main.MouseScreen.ToPoint() );
			bool isAlt = Main.keyState.IsKeyDown( Keys.LeftAlt )
				|| Main.keyState.IsKeyDown( Keys.RightAlt );

			if( Main.mouseLeft && isAlt ) {
				if( this.BaseDragOffset.HasValue || isHovering ) {
					this.RunHUDEditor_Drag( pos );
				}
			} else {
				this.BaseDragOffset = null;
			}

			return this.BaseDragOffset.HasValue;
		}


		private void RunHUDEditor_Drag( Vector2 basePos ) {
			if( !this.BaseDragOffset.HasValue ) {
				this.BaseDragOffset = basePos - Main.MouseScreen;
				this.PreviousDragMousePos = Main.MouseScreen;

				return;
			}

			var mymod = ModContent.GetInstance<HUDElementsLibMod>();

			Vector2 movedSince = Main.MouseScreen - this.PreviousDragMousePos;
			this.PreviousDragMousePos = Main.MouseScreen;

			this.DesiredPosition += movedSince;

			Vector2 validatedPosition = default;

			for( int i=0; i<10; i++ ) {	// <- lazy
				Vector2 testValidatedPosition = mymod.HUDManager.HandleFirstFoundCollision( this );
				if( testValidatedPosition == validatedPosition ) {
					break;
				}

				validatedPosition = testValidatedPosition;
			}

			this.Left.Pixels += validatedPosition.X;
			this.Top.Pixels += validatedPosition.Y;
		}
	}
}