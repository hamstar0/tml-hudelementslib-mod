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
			int count = tag.GetInt( "hud_element_count" );

			for( int i=0; i<count; i++ ) {
				string name = tag.GetString( "hud_element_"+i );
				float x = tag.GetFloat( "hud_element_x_"+i );
				float y = tag.GetFloat( "hud_element_y_"+i );

				mymod.LoadHUDElementAsInfo( name, x, y );
			}
		}

		public override TagCompound Save() {
			var mymod = ModContent.GetInstance<HUDElementsLibMod>();
			var tag = new TagCompound {
				{ "hud_element_count", mymod.SavedElementInfo.Count }
			};

			int i = 0;
			foreach( string name in mymod.Elements.Keys ) {
				HUDElement elem = mymod.Elements[ name ];

				tag[ "hud_element_"+i ] = name;
				tag[ "hud_element_x_"+i ] = elem.Left.Pixels;
				tag[ "hud_element_y_"+i ] = elem.Top.Pixels;
				i++;
			} 

			return tag;
		}
	}
}