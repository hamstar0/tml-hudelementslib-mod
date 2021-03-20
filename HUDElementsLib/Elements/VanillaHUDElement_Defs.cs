using System;
using Microsoft.Xna.Framework;
using Terraria;
using HUDElementsLib.Libraries.Helpers.HUD;


namespace HUDElementsLib {
	public partial class VanillaHUDElement : HUDElement {
		static VanillaHUDElement() {
			VanillaHUDElement.VanillaHUDInfo = new VanillaHUDElementDefinition[] {
				new VanillaHUDElementDefinition(
					name: "Inventory Hotbar",
					context: () => true,
					position: () => new Vector2( 20, 18 ),		// Main.inventoryScale,
					dimensions: () => new Vector2( 472, 48 ),	// Main.inventoryScale,
					displacement: () => new Vector2( 1, 0 )
				),
				new VanillaHUDElementDefinition(
					name: "Inventory",
					context: () => Main.playerInventory,
					position: () => new Vector2( 20, 66 ),		// Main.inventoryScale,
					dimensions: () => new Vector2( 472, 190 ),	// Main.inventoryScale,
					displacement: () => new Vector2( 0, 1 )
				),
				new VanillaHUDElementDefinition(
					name: "Inventory Chest",
					context: () => Main.playerInventory && Main.LocalPlayer.chest != -1,
					position: () => new Vector2( 68, 256 ),		// Main.inventoryScale,
					dimensions: () => new Vector2( 424, 170 ),	// Main.inventoryScale,
					displacement: () => new Vector2( 0, 1 )
				),
				new VanillaHUDElementDefinition(
					name: "Life Bar",
					context: () => true,
					position: () => new Vector2( -302, 4 ),
					dimensions: () => new Vector2( 260, 78 ),
					displacement: () => new Vector2( -1, 0 )
				),
				new VanillaHUDElementDefinition(
					name: "Armor And Accessories",
					context: () => Main.playerInventory,
					position: () => {
						Vector2 topLeft = HUDElementHelpers.GetVanillaAccessorySlotScreenPosition(0);
						return new Vector2( -188, topLeft.Y-178 );
					},
					dimensions: () => {
						if( Main.LocalPlayer.extraAccessory ) {
							return new Vector2( 144, 418+48 );
						}
						return new Vector2( 144, 418 );
					},
					displacement: () => new Vector2( -1, 0 )
				),
				new VanillaHUDElementDefinition(
					name: "Map Buttons",
					context: () => Main.playerInventory,
					position: () => {
						if( Main.screenWidth < 940 ) {
							return new Vector2( -40, -192 );
						}
						return new Vector2( -440, 38 );
					},
					dimensions: () => {
						if( Main.screenWidth < 940 ) {
							return new Vector2( 36, 130 );
						}
						return new Vector2( 130, 36 );
					},
					displacement: () => new Vector2( -1, 0 )
				),
				new VanillaHUDElementDefinition(
					name: "Mini Map",
					context: () => Main.mapStyle == 1,
					position: () => new Vector2( -298, 84 ),
					dimensions: () => new Vector2( 252, 252 )
				),
				new VanillaHUDElementDefinition(
					name: "Trash Slot",
					context: () => Main.playerInventory,
					position: () => new Vector2( 444, 256 ),	// Main.inventoryScale,
					dimensions: () => new Vector2( 48, 48 ),	// Main.inventoryScale,
					displacement: () => new Vector2( -1, 0 )
				),
				new VanillaHUDElementDefinition(
					name: "Money & Ammo Slots",
					context: () => Main.playerInventory,
					position: () => new Vector2( 494, 82 ),		// Main.inventoryScale,
					dimensions: () => new Vector2( 68, 192 ),	// Main.inventoryScale,
					displacement: () => new Vector2( 1, 0 )
				),
				new VanillaHUDElementDefinition(
					name: "Info Accessory Buttons",
					context: () => Main.playerInventory,
					position: () => {
						if( Main.screenWidth < 855 || Main.mapStyle == 1 ) {
							return new Vector2( -228, 320 );
						}
						return new Vector2( -302, 82 );
					},
					dimensions: () => {
						if( Main.screenWidth < 855 || Main.mapStyle == 1 ) {
							return new Vector2( 36, 144 );
						}
						return new Vector2( 260, 24 );
					},
					displacement: () => {
						if( Main.screenWidth < 855 || Main.mapStyle == 1 ) {
							return new Vector2( 1, 0 );
						}
						return new Vector2( 0, 1 );
					}
				),
				new VanillaHUDElementDefinition(
					name: "Crafting",
					context: () => Main.playerInventory,
					position: () => new Vector2( 20, -272 ),	// Main.inventoryScale,
					dimensions: () => new Vector2( 96, 304 ),	// Main.inventoryScale,
					displacement: () => new Vector2( 1, -1 )
				),
			};
		}



		////////////////
		
		internal static void LoadVanillaElements() {
			foreach( VanillaHUDElementDefinition def in VanillaHUDElement.VanillaHUDInfo ) {
				var elem = new VanillaHUDElement( def );

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