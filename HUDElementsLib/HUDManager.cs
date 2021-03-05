using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	partial class HUDManager {
		public static IReadOnlyDictionary<string, Func<Rectangle>> VanillaHUD { get; private set; }


		static HUDManager() {
			HUDManager.VanillaHUD = new ReadOnlyDictionary<string, Func<Rectangle>>(
				new Dictionary<string, Func<Rectangle>> {
					{ "InventoryHotbar", () => new Rectangle() },
					{ "Inventory", () => new Rectangle() },
					{ "InventoryChest", () => new Rectangle() },
					{ "LifeBar", () => new Rectangle() },
					{ "ArmorAndAccessories", () => new Rectangle() },
					{ "MapButtons", () => new Rectangle() },
					{ "MinimMap", () => new Rectangle() }
				}
			);
		}



		////////////////

		//internal IDictionary<string, ISet<HUDElement>> Elements = new Dictionary<string, ISet<HUDElement>>();
		public IDictionary<string, HUDElement> Elements = new Dictionary<string, HUDElement>();
		public IDictionary<string, (float x, float y)> SavedElementInfo = new Dictionary<string, (float, float)>();

		private UIState MyUI;



		////////////////

		public HUDManager( UIState myUI ) {
			this.MyUI = myUI;
		}


		////////////////
		
		public void LoadHUDElementInfo( string name, float x, float y ) {
			if( this.Elements.ContainsKey(name) ) {
				this.Elements[name].Left.Pixels = x;
				this.Elements[name].Top.Pixels = y;
			} else {
				this.SavedElementInfo[name] = (x, y);
			}
		}

		public void LoadHUDElement( HUDElement element ) {
			this.Elements[ element.Name ] = element;

			if( this.SavedElementInfo.ContainsKey(element.Name) ) {
				(float x, float y) elemInfo = this.SavedElementInfo[ element.Name ];
				this.SavedElementInfo.Remove( element.Name );

				element.Left.Pixels = elemInfo.x;
				element.Top.Pixels = elemInfo.y;
			}
		}
	}
}