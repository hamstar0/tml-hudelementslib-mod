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

			ModContent.GetInstance<HUDElementsLibMod>()
				.HUDManager
				.ApplyDisplacementsIf( this );

			if( Main.playerInventory ) {
				this.UpdateInteractionsIf( out bool isHovering );
				this.IsHovering = isHovering;
			} else {
				this.IsHovering = false;

				this.DesiredDragPosition = null;
			}

			this.UpdateHUDPosition();
		}

		////

		private void UpdateHUDPosition() {
			Vector2 pos = this.GetCustomPositionOnHUD( false );
			Vector2 dim = this.GetCustomDimensionsOnHUD();

			this.Left.Pixels = pos.X;
			this.Top.Pixels = pos.Y;
			this.Width.Pixels = dim.X;
			this.Height.Pixels = dim.Y;
		}
	}
}