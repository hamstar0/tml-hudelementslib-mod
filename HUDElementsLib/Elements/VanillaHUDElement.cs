using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using Terraria;


namespace HUDElementsLib {
	public partial class VanillaHUDElement : HUDElement {
		public static IReadOnlyDictionary<string, (Func<bool> context, Rectangle area)> VanillaHUDInfo { get; private set; }


		static VanillaHUDElement() {
			var dict = new Dictionary<string, (Func<bool> context, Rectangle area)> {
				{
					"Inventory Hotbar",
					(
						() => true,
						new Rectangle()
					)
				},
				{
					"Inventory",
					(
						() => Main.playerInventory,
						new Rectangle()
					)
				},
				{
					"Inventory Chest",
					(
						() => Main.playerInventory && Main.LocalPlayer.chest != -1,
						new Rectangle()
					)
				},
				{
					"Life Bar",
					(
						() => true,
						new Rectangle()
					)
				},
				{
					"Armor And Accessories",
					(
						() => Main.playerInventory,
						new Rectangle()
					)
				},
				{
					"Map Buttons",
					(
						() => Main.playerInventory,
						new Rectangle()
					)
				},
				{
					"Mini Map",
					(
						() => Main.mapStyle == 1,
						new Rectangle()
					)
				}
			};
			VanillaHUDElement.VanillaHUDInfo = new ReadOnlyDictionary<string, (Func<bool> context, Rectangle area)>( dict );
		}



		////////////////

		internal VanillaHUDElement( string name ) : base( name ) { }


		public override bool IsAnchored() => true;
	}
}