using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public override void Update( GameTime gameTime ) {
			if( !this.IsEnabled() ) {
				return;
			}

			base.Update( gameTime );

			//int oldMouseX = Main.mouseX;
			//int oldMouseY = Main.mouseY;
			//Main.mouseX = (int)( (float)Main.mouseX * (Main.GameZoomTarget * Main.UIScale) );
			//Main.mouseY = (int)( (float)Main.mouseY * (Main.GameZoomTarget * Main.UIScale) );

			var mymod = ModContent.GetInstance<HUDElementsLibMod>();
			mymod.HUDManager.ApplyDisplacementsIf( this );

			if( Main.playerInventory && this.IsInteractive() ) {
				this.UpdateInteractionsIf( out bool isHovering );

				this.IsHovering = isHovering;
			} else {
				this.IsHovering = false;

				this.DesiredDragPosition = null;
			}

			this.UpdateHUDPosition();

			//Main.mouseX = oldMouseX;
			//Main.mouseY = oldMouseY;
		}

		////

		private void UpdateHUDPosition() {
			Vector2 pos = this.GetHUDComputedPosition( true );
			Vector2 dim = this.GetHUDComputedDimensions();

			this.Left.Pixels = pos.X;
			this.Top.Pixels = pos.Y;
			this.Width.Pixels = dim.X;
			this.Height.Pixels = dim.Y;
		}
	}
}
