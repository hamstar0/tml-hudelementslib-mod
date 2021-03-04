using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;


namespace HUDElementsLib {
	public partial class HUDElementsLibMod : Mod {
		public override void UpdateUI( GameTime gameTime ) {
			this.MyUI?.Update( gameTime );

			foreach( HUDElement elem in this.HUDManager.Elements.Values ) {
				elem.Update( gameTime );

				if( elem.IsDragging ) {
					Main.LocalPlayer.mouseInterface = true;
					break;
				}
			}
		}


		////////////////

		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			GameInterfaceDrawMethod widgetsUI = delegate {
				//this.MyUI?.Update( gameTime );
				this.MyUI?.Draw( Main.spriteBatch );
				return true;
			};

			var invOverLayer = new LegacyGameInterfaceLayer( "HUD Elements: Widgets", widgetsUI, InterfaceScaleType.UI );
			layers.Add( invOverLayer );

			//

			int cursorIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Cursor" ) );
			if( cursorIdx == -1 ) { return; }

			foreach( HUDElement elem in this.HUDManager.Elements.Values ) {
				if( elem.ConsumesCursor() ) {
					layers.RemoveAt( cursorIdx );
					break;
				}
			}
		}
	}
}