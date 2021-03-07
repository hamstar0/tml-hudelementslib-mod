using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using Terraria;


namespace HUDElementsLib {
	public partial class VanillaHUDElement : HUDElement {
		static VanillaHUDElement() {
			var dict = new Dictionary<string, (Func<bool> context, Rectangle area)> {
				{
					"Inventory Hotbar",
					(
						() => true,
						new Rectangle( 16, 16, 320, 32 )
					)
				},
				{
					"Inventory",
					(
						() => Main.playerInventory,
						new Rectangle( 16, 48, 320, 160 )
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
						new Rectangle( -256, 16, 256, 48 )
					)
				},
				{
					"Armor And Accessories",
					(
						() => Main.playerInventory,
						new Rectangle( -128, -312, 128, 312 )
					)
				},
				{
					"Map Buttons",
					(
						() => Main.playerInventory,
						new Rectangle( -368, 16, 128, 32 )
					)
				},
				{
					"Mini Map",
					(
						() => Main.mapStyle == 1,
						new Rectangle( -192, 16, 192, 192 )
					)
				}
			};
			VanillaHUDElement.VanillaHUDInfo = new ReadOnlyDictionary<string, (Func<bool> context, Rectangle area)>( dict );
		}



		////////////////
		
		internal static void LoadVanillaElements() {
			foreach( KeyValuePair<string, (Func<bool> context, Rectangle area)> kv in VanillaHUDElement.VanillaHUDInfo ) {
				var elem = new VanillaHUDElement( kv.Key, kv.Value.context, kv.Value.area );

//				HUDElementsLibAPI.AddWidget( elem );
			}
		}
	}
}