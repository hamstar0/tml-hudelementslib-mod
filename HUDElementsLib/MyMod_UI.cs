using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;


namespace HUDElementsLib {
	public partial class HUDElementsLibMod : Mod {
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
				this.MyUI?.Draw( Main.spriteBatch );
				return true;
			};

			var invOverLayer = new LegacyGameInterfaceLayer( "HUDElementsLib: Widgets", widgetsUI, InterfaceScaleType.UI );
			layers.Add( invOverLayer );

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
		}
	}
}