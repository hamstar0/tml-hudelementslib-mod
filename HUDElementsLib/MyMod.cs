using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;


namespace HUDElementsLib {
	public partial class HUDElementsLibMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-hudelementslib-mod";



		////////////////

		//internal IDictionary<string, ISet<HUDElement>> Elements = new Dictionary<string, ISet<HUDElement>>();
		internal IDictionary<string, HUDElement> Elements = new Dictionary<string, HUDElement>();
		internal IDictionary<string, (float x, float y)> SavedElementInfo = new Dictionary<string, (float, float)>();

		private UserInterface MyUIMngr;
		internal UIState MyUI;



		////////////////

		public override void Load() {
			if( !Main.dedServ ) {
				this.MyUIMngr = new UserInterface();
				this.MyUI = new UIState();
				this.MyUIMngr.SetState( this.MyUI );
			}
		}


		////////////////
		
		internal void LoadHUDElementAsInfo( string name, float x, float y ) {
			if( this.Elements.ContainsKey(name) ) {
				this.Elements[name].Left.Pixels = x;
				this.Elements[name].Top.Pixels = y;
			} else {
				this.SavedElementInfo[name] = (x, y);
			}
		}
		
		internal void LoadHUDElementFromInfo( string name, HUDElement element ) {
			if( !this.SavedElementInfo.ContainsKey(name) ) {
				return;
			}

			(float x, float y) elemInfo = this.SavedElementInfo[name];
			this.SavedElementInfo.Remove( name );

			element.Left.Pixels = elemInfo.x;
			element.Top.Pixels = elemInfo.y;
		}
	}
}