using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace HUDElementsLib {
	class HUDElementsLibPlayer : ModPlayer {
		public override bool CloneNewInstances => false;



		////////////////

		public override void Load( TagCompound tag ) {
			if( !tag.ContainsKey("hud_element_count") ) {
				return;
			}

			//

			var mymod = ModContent.GetInstance<HUDElementsLibMod>();
			HUDManager hudMngr = mymod.HUDManager;

			//

			int count = tag.GetInt( "hud_element_count" );

			for( int i=0; i<count; i++ ) {
				string name = tag.GetString( $"hud_element_{i}" );
				float x = tag.GetFloat( $"hud_element_x_{i}" );
				float y = tag.GetFloat( $"hud_element_y_{i}" );
				bool hasCollisionData = tag.ContainsKey( $"hud_element_cc_{i}")
					? (bool)tag.GetBool( $"hud_element_cc_{i}" )
					: false;
				bool? isIgnoringCollisions = hasCollisionData
					? (bool?)tag.GetBool( $"hud_element_c_{i}" )
					: (bool?)null;

				hudMngr.LoadHUDElementInfo( name, x, y, isIgnoringCollisions );
			}
		}

		public override TagCompound Save() {
			var mymod = ModContent.GetInstance<HUDElementsLibMod>();
			HUDManager hudMngr = mymod.HUDManager;

			IEnumerable<HUDElement> elements = hudMngr.Elements
				.Where( kv => !kv.Value.SkipSave() )
				.Select( kv => kv.Value );

			//

			var tag = new TagCompound {
				{ "hud_element_count", elements.Count() + hudMngr.SavedElementInfo.Count }
			};

			int i = 0;

			foreach( HUDElement elem in elements ) {
				tag[ $"hud_element_{i}" ] = elem.Name;
				tag[ $"hud_element_x_{i}" ] = (float)elem.GetPositionAndAnchors().X;
				tag[ $"hud_element_y_{i}" ] = (float)elem.GetPositionAndAnchors().Y;
				tag[ $"hud_element_c_{i}" ] = (bool)elem.IsIgnoringCollisions;
				tag[ $"hud_element_cc_{i}" ] = true;

				i++;
			}

			foreach( string name in hudMngr.SavedElementInfo.Keys ) {
				HUDManager.ElementInfo elemInfo = hudMngr.SavedElementInfo[ name ];

				tag[ $"hud_element_{i}" ] = name;
				tag[ $"hud_element_x_{i}" ] = (float)elemInfo.ScreenPosition.X;
				tag[ $"hud_element_y_{i}" ] = (float)elemInfo.ScreenPosition.Y;
				tag[ $"hud_element_c_{i}" ] = elemInfo.IsIgnoringCollisions.HasValue
					? (bool)elemInfo.IsIgnoringCollisions.Value
					: false;
				tag[ $"hud_element_cc_{i}" ] = (bool)elemInfo.IsIgnoringCollisions.HasValue;

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