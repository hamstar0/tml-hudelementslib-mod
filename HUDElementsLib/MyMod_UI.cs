using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;


namespace HUDElementsLib {
	public partial class HUDElementsLibMod : Mod {
		public static readonly string[] LayerNames = new string[] {
			"Vanilla: Tile Grid Option",
			"Vanilla: Town NPC House Banners",
			"Vanilla: Hide UI Toggle",
			"Vanilla: Wire Selection",
			"Vanilla: Ingame Options",
			"Vanilla: Fancy UI",
			"Vanilla: Achievement Complete Popups",
			"Vanilla: Invasion Progress Bars",
			"Vanilla: Map / Minimap",
			"Vanilla: Hair Window",
			"Vanilla: Dresser Window",
			"Vanilla: NPC / Sign Dialog",
			"Vanilla: Resource Bars",
			"Vanilla: Inventory",
			"Vanilla: Info Accessories Bar",
			"Vanilla: Settings Button",
			"Vanilla: Hotbar",
			"Vanilla: Builder Accessories Bar",
			"Vanilla: Radial Hotbars",
			"Vanilla: Player Chat",
		};



		////////////////

		/*public override void UpdateUI( GameTime gameTime ) {
			//bool mouseLeft = PlayerInput.Triggers.Current.MouseLeft;
			//bool isAlt = Main.keyState.IsKeyDown( Keys.LeftAlt ) || Main.keyState.IsKeyDown( Keys.RightAlt );
			//this.Logger.Info( "lclick:"+mouseLeft+" ("+Main.mouseLeft+"), alt:"+isAlt+", gameTime:"+gameTime.ElapsedGameTime.Ticks );
			//this.MyUI?.Update( gameTime );

			foreach( HUDElement elem in this.HUDManager.Elements.Values ) {
				if( elem.IsDragging ) {
//this.Logger.Info( "drag " + gameTime.ElapsedGameTime.Ticks+", thread: "+Thread.CurrentThread.ManagedThreadId);
					Main.LocalPlayer.mouseInterface = true;		<- This fails because UpdateUI runs before inputs are processed
					break;
				}
			}
		}*/


		////////////////

		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			GameInterfaceDrawMethod widgetsUI = delegate {
				this.MyUI?.Update( Main._drawInterfaceGameTime );  //:blobshrug:
				this.MyUI?.Draw( Main.spriteBatch );
				return true;
			};

			int mouseTextIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Mouse Text" ) );
			if( mouseTextIdx >= 0 ) {
				var invOverLayer = new LegacyGameInterfaceLayer( "HUDElementsLib: Widgets", widgetsUI, InterfaceScaleType.UI );
				layers.Insert( mouseTextIdx, invOverLayer );
			}

			//

			int cursorIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Cursor" ) );
			if( cursorIdx >= 0 ) {
				foreach( HUDElement elem in this.HUDManager.Elements.Values ) {
					if( elem.ConsumesCursor() ) {
						layers.RemoveAt( cursorIdx );
						break;
					}
				}
			}

			if( HUDElementsLibAPI.IsEditModeActive() ) {
				this.DisableHUDInterfaceLayers( layers );
			}
		}


		////

		private void DisableHUDInterfaceLayers( List<GameInterfaceLayer> layers ) {
			foreach( string layerName in HUDElementsLibMod.LayerNames ) {
				int idx = layers.FindIndex( layer => layer.Name.Equals(layerName) );
				if( idx <= -1 ) {
					continue;
				}

				layers.RemoveAt( idx );
			}
		}
	}
}