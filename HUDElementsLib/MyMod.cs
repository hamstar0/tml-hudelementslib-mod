using Terraria;
using Terraria.UI;
using Terraria.ModLoader;


namespace HUDElementsLib {
	public partial class HUDElementsLibMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-hudelementslib-mod";



		////////////////

		internal HUDManager HUDManager;

		private UserInterface MyUIMngr;
		internal UIState MyUI;



		////////////////

		public override void Load() {
			if( !Main.dedServ ) {
				this.MyUIMngr = new UserInterface();
				this.MyUI = new UIState();
				this.MyUIMngr.SetState( this.MyUI );

				this.HUDManager = new HUDManager( this.MyUI );
			}
		}
	}
}