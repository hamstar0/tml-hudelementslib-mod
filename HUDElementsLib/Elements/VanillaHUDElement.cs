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
		
		internal static void LoadVanillaElements() {
			foreach( KeyValuePair<string, (Func<bool> context, Rectangle area)> kv in VanillaHUDElement.VanillaHUDInfo ) {
				var elem = new VanillaHUDElement( kv.Key, kv.Value.context, kv.Value.area );

				HUDElementsLibAPI.AddWidget( elem );
			}
		}



		////////////////

		public Func<bool> EnablingCondition;



		////////////////

		internal VanillaHUDElement( string name, Func<bool> enablingCondition, Rectangle area ) : base( name ) {
			this.EnablingCondition = enablingCondition;
			this.Left.Pixels = area.X;
			this.Left.Precent = area.X < 0f ? 1f : 0f;
			this.Top.Pixels = area.Y;
			this.Top.Precent = area.Y < 0f ? 1f : 0f;
			this.Width.Pixels = area.Width;
			this.Height.Pixels = area.Height;
		}


		public override bool IsAnchored() => true;

		public override bool IsLocked() => true;

		public override bool IsEnabled() => this.EnablingCondition.Invoke();
	}
}