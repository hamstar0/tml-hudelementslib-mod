using System.Collections.Generic;
using System.Linq;
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
			IEnumerable<HUDElement> elements = hudMngr.Elements
				.Where( kv => !kv.Value.SkipSave() )
				.Select( kv => kv.Value );

			var tag = new TagCompound {
				{ "hud_element_count", elements.Count() + hudMngr.SavedElementInfo.Count }
			};

			int i = 0;

			foreach( HUDElement elem in elements ) {
				tag[ "hud_element_"+i ] = elem.Name;
				tag[ "hud_element_x_"+i ] = (float)elem.GetPositionAndAnchors().X;
				tag[ "hud_element_y_"+i ] = (float)elem.GetPositionAndAnchors().Y;
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
			mymod.HUDManager?.PreUpdateForInteractions();  //:blobshrug:
		}
	}
}