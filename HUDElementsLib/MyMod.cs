using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.ModLoader;


namespace HUDElementsLib {
	public partial class HUDElementsLibMod : Mod {
		public static HUDElementsLibMod Instance => ModContent.GetInstance<HUDElementsLibMod>();



		////////////////

		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-hudelementslib-mod";



		////////////////

		internal HUDManager HUDManager;

		private UserInterface MyUIMngr;
		internal UIState MyUI;

		////////////////

		internal ModHotKey HUDEditMode = null;
		internal ModHotKey HUDReset = null;



		////////////////

		public override void Load() {
			this.HUDEditMode = this.RegisterHotKey( "Edit Mode", "O" );
			this.HUDReset = this.RegisterHotKey( "Reset Elements", "P" );

			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				this.MyUIMngr = new UserInterface();
				this.MyUI = new UIState();
				this.MyUIMngr.SetState( this.MyUI );

				this.HUDManager = new HUDManager( this.MyUI );
				VanillaHUDElement.LoadVanillaElements();
			}
		}
	}
}