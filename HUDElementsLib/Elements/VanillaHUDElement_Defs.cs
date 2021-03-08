using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;


namespace HUDElementsLib {
	public partial class VanillaHUDElement : HUDElement {
		static VanillaHUDElement() {
			var dict = new Dictionary<string, (Func<bool> context, Rectangle area)> {
				{
					"Inventory Hotbar",
					(
						() => true,
						new Rectangle( 20, 18, 472, 48 )
					)
				},
				{
					"Inventory",
					(
						() => Main.playerInventory,
						new Rectangle( 20, 66, 472, 192 )
					)
				},
				{
					"Inventory Chest",
					(
						() => Main.playerInventory && Main.LocalPlayer.chest != -1,
						new Rectangle( 16, 48+320, 320, 32 )
					)
				},
				{
					"Life Bar",
					(
						() => true,
						new Rectangle( -302, 4, 256, 76 )
					)
				},
				{
					"Armor And Accessories",
					(
						() => Main.playerInventory,
						new Rectangle( -184, 132, 144, 416 )
					)
				},
				{
					"Map Buttons",
					(
						() => Main.playerInventory,
						new Rectangle( -440, 40, 126, 32 )
					)
				},
				{
					"Mini Map",
					(
						() => Main.mapStyle == 1,
						new Rectangle( -314, 136, 224, 224 )
					)
				}
			};
			VanillaHUDElement.VanillaHUDInfo = new ReadOnlyDictionary<string, (Func<bool> context, Rectangle area)>( dict );
		}



		////////////////
		
		internal static void LoadVanillaElements() {
			foreach( KeyValuePair<string, (Func<bool> context, Rectangle area)> kv in VanillaHUDElement.VanillaHUDInfo ) {
				var elem = new VanillaHUDElement( kv.Key, kv.Value.context, kv.Value.area );

				HUDElementsLibAPI.AddWidget( elem );
			}
		}


		/*public override void Draw( SpriteBatch sb ) {
			base.Draw( sb );

			sb.DrawString(
				spriteFont: Main.fontMouseText,
				text: this.GetAreaOnHUD( false ).ToString(),
				position: this.GetPositionOnHUD( false ) + new Vector2( 4 ),
				color: Color.White
			);
		}*/
	}
}