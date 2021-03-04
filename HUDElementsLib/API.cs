using Terraria;
using Terraria.ModLoader;


namespace HUDElementsLib {
	public static class HUDElementsLibAPI {
		public static HUDElement GetDraggingElement() {
			var mymod = ModContent.GetInstance<HUDElementsLibMod>();

			foreach( HUDElement elem in mymod.HUDManager.Elements.Values ) {
				if( elem.IsDragging ) {
					return elem;
				}
			}

			return null;
		}


		////////////////

		public static void AddWidget( HUDElement element ) {    //string layerName = "Vanilla: Mouse Text"
			var mymod = ModContent.GetInstance<HUDElementsLibMod>();

			//if( !mymod.Elements.ContainsKey(layerName) ) {
			//	mymod.Elements[ layerName ] = new HashSet<HUDElement>();
			//}

			mymod.HUDManager.LoadHUDElementFromInfo( element );

			mymod.MyUI?.Append( element );
			mymod.MyUI?.Recalculate();
		}
	}
}