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
			if( !tag.ContainsKey("hud_elements_count") ) {
				return;
			}

			//

			var mymod = ModContent.GetInstance<HUDElementsLibMod>();
			HUDManager hudMngr = mymod.HUDManager;

			//

			int count = tag.GetInt( "hud_elements_count" );

			for( int i=0; i<count; i++ ) {
				string name = tag.GetString( $"hud_elem_{i}" );
				float relX = tag.GetFloat( $"hud_elem_rx_{i}" );
				float relY = tag.GetFloat( $"hud_elem_ry_{i}" );
				float percX = tag.GetFloat( $"hud_elem_px_{i}" );
				float percY = tag.GetFloat( $"hud_elem_py_{i}" );
				bool hasCollisionData = tag.ContainsKey( $"hud_elem_cc_{i}")
					? (bool)tag.GetBool( $"hud_elem_cc_{i}" )
					: false;
				bool? isIgnoringCollisions = hasCollisionData
					? (bool?)tag.GetBool( $"hud_elem_c_{i}" )
					: (bool?)null;

				hudMngr.LoadHUDElementInfo( name, new Vector2(relX, relY), new Vector2(percX, percY), isIgnoringCollisions );
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
				{ "hud_elements_count", elements.Count() + hudMngr.SavedElementInfo.Count }
			};

			int i = 0;

			foreach( HUDElement elem in elements ) {
				(Vector2 relPos, Vector2 percPos) = elem.GetIntendedPosition();

				tag[ $"hud_elem_{i}" ] = elem.Name;
				tag[ $"hud_elem_rx_{i}" ] = (float)relPos.X;
				tag[ $"hud_elem_ry_{i}" ] = (float)relPos.Y;
				tag[ $"hud_elem_px_{i}" ] = (float)percPos.X;
				tag[ $"hud_elem_py_{i}" ] = (float)percPos.Y;
				tag[ $"hud_elem_c_{i}" ] = (bool)elem.IsIgnoringCollisions;
				tag[ $"hud_elem_cc_{i}" ] = true;

				i++;
			}

			foreach( string name in hudMngr.SavedElementInfo.Keys ) {
				HUDManager.ElementInfo elemInfo = hudMngr.SavedElementInfo[ name ];

				tag[ $"hud_elem_{i}" ] = name;
				tag[ $"hud_elem_rx_{i}" ] = (float)elemInfo.RelativePosition.X;
				tag[ $"hud_elem_ry_{i}" ] = (float)elemInfo.RelativePosition.Y;
				tag[ $"hud_elem_px_{i}" ] = (float)elemInfo.PositionPercent.X;
				tag[ $"hud_elem_py_{i}" ] = (float)elemInfo.PositionPercent.Y;
				tag[ $"hud_elem_c_{i}" ] = elemInfo.IsIgnoringCollisions.HasValue
					? (bool)elemInfo.IsIgnoringCollisions.Value
					: false;
				tag[ $"hud_elem_cc_{i}" ] = (bool)elemInfo.IsIgnoringCollisions.HasValue;

				i++;
			}

			return tag;
		}


		////////////////

		/*public override void PreUpdate() {
			if( this.player.whoAmI == Main.myPlayer ) {
				this.PreUpdate_Local();
			}
		}

		////

		private void PreUpdate_Local() {
			var mymod = ModContent.GetInstance<HUDElementsLibMod>();
			Vector2 mouseScrPos = Main.MouseScreen;

			mymod.HUDManager?.PreUpdateForInteractions( mouseScrPos );  //:blobshrug:
		}*/
	}
}