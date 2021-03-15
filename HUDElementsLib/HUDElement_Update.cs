using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		internal bool PreUpdateForInteractions() {
			//Rectangle area = this.GetHUDComputedArea( false );	<- Incorrect rectangle when game or ui zooms
			//this.IsMouseHovering_Custom = area.Contains( Main.MouseScreen.ToPoint() );

			bool isInteracting = Main.playerInventory
				&& Main.mouseLeft
				&& this.IsMouseHovering_Custom;

			if( isInteracting ) {
				Main.LocalPlayer.mouseInterface = true; // Locks control for this element
			}

			return isInteracting;
		}


		////

		public override void Update( GameTime gameTime ) {
			if( !this.IsEnabled() ) {
				return;
			}

			base.Update( gameTime );

			Rectangle area = this.GetHUDComputedArea( false );  // Original spot only
			this.IsMouseHovering_Custom = area.Contains( Main.MouseScreen.ToPoint() );

			this.UpdateHUD();
		}

		private void UpdateHUD() {
			var mymod = ModContent.GetInstance<HUDElementsLibMod>();
			mymod.HUDManager.ApplyDisplacementsIf( this );

			if( Main.playerInventory && this.IsInteractive() ) {
				this.UpdateInteractions();
			} else {
				this.ResetInteractions();
			}

			this.UpdateHUDPosition();
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
