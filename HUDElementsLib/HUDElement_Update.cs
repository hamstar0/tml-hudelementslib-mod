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

			//

			Rectangle area = this.GetHUDComputedArea( false );  // Original spot only
			this.IsMouseHoveringEditableBox = area.Contains( Main.mouseX, Main.mouseY );
/*ModLibsCore.Libraries.Debug.DebugLibraries.Print(
	"upd_2_"+this.Name,
	"i:"+this.IsMouseHoveringEditableBox+", a:"+area+", x:"+Main.mouseX+", y:"+Main.mouseY
);*/

			//

			this.UpdateHUD();
		}

		private void UpdateHUD() {
			var mymod = ModContent.GetInstance<HUDElementsLibMod>();
			mymod.HUDManager.FindAndApplyDisplacements( this );

			if( !this.UpdateEditModeInteractions_If() ) {
				this.ResetEditModeInteractions();
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
