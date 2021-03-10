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

			mymod.HUDManager.LoadHUDElement( element );

			mymod.MyUI?.Append( element );
			mymod.MyUI?.Recalculate();
		}
	}
}