using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace HUDElementsLib {
	class HUDElementsLibPlayer : ModPlayer {
		public override void Load( TagCompound tag ) {
			if( !tag.ContainsKey("hud_element_count") ) {
				return;
			}

			var mymod = ModContent.GetInstance<HUDElementsLibMod>();
			HUDManager hudMngr = mymod.HUDManager;

			int count = tag.GetInt( "hud_element_count" );

			for( int i=0; i<count; i++ ) {
				string name = tag.GetString( "hud_element_"+i );
				float x = tag.GetFloat( "hud_element_x_"+i );
				float y = tag.GetFloat( "hud_element_y_"+i );

				hudMngr.LoadHUDElementInfo( name, x, y );
			}
		}

		public override TagCompound Save() {
			var mymod = ModContent.GetInstance<HUDElementsLibMod>();
			HUDManager hudMngr = mymod.HUDManager;

			var tag = new TagCompound {
				{ "hud_element_count", hudMngr.Elements.Count + hudMngr.SavedElementInfo.Count }
			};

			int i = 0;

			foreach( string name in hudMngr.Elements.Keys ) {
				HUDElement elem = hudMngr.Elements[ name ];

				tag[ "hud_element_"+i ] = name;
				tag[ "hud_element_x_"+i ] = (float)elem.GetPositionOnHUD( true ).X;
				tag[ "hud_element_y_"+i ] = (float)elem.GetPositionOnHUD( true ).Y;
				i++;
			}

			foreach( string name in hudMngr.SavedElementInfo.Keys ) {
				Vector2 elemInfo = hudMngr.SavedElementInfo[ name ];

				tag[ "hud_element_"+i ] = name;
				tag[ "hud_element_x_"+i ] = (float)elemInfo.X;
				tag[ "hud_element_y_"+i ] = (float)elemInfo.Y;
				i++;
			}

			return tag;
		}


		////////////////

		public override void PreUpdate() {
			if( this.player.whoAmI == Main.myPlayer ) {
				this.PreUpdateLocal();
			}
		}

		////

		private void PreUpdateLocal() {
			var mymod = ModContent.GetInstance<HUDElementsLibMod>();
			mymod.MyUI?.Update( Main._drawInterfaceGameTime );  //:blobshrug:
		}
	}
}