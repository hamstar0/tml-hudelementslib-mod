using System;
using Terraria;
using Terraria.ModLoader;


namespace HUDElementsLib {
	public static class HUDElementsLibAPI {
		private static void MessageAboutHUD_WeakRef() {
			Messages.MessagesAPI.AddMessagesCategoriesInitializeEvent( () => {
				string id = "HUDElementsLibUsage";

				Messages.MessagesAPI.AddMessage(
					title: "Reposition HUD elements via. hotkey",
					description: "Bind a key to activate Edit Mode to reposition custom HUD elements freely to your liking.",
					modOfOrigin: HUDElementsLibMod.Instance,
					id: id,
					alertPlayer: Messages.MessagesAPI.IsUnread(id),
					isImportant: false,
					parentMessage: Messages.MessagesAPI.ModInfoCategoryMsg
				);
			} );
		}


		////////////////

		public static bool IsEditModeActive() {
			return HUDElementsLibMod.Instance.HUDEditMode.Current;
		}


		////////////////
		
		public static HUDElement GetDraggingElement() {
			var mymod = ModContent.GetInstance<HUDElementsLibMod>();

			foreach( HUDElement elem in mymod.HUDManager.Elements.Values ) {
				if( elem.IsDraggingSinceLastTick ) {
					return elem;
				}
			}

			return null;
		}


		////////////////

		public static void AddWidget( HUDElement element ) {    //string layerName = "Vanilla: Mouse Text"
			var mymod = ModContent.GetInstance<HUDElementsLibMod>();

			mymod.HUDManager?.LoadHUDElement( element );

			mymod.MyUI?.Append( element );
			mymod.MyUI?.Recalculate();

			if( ModLoader.GetMod("Messages") != null ) {
				HUDElementsLibAPI.MessageAboutHUD_WeakRef();
			}
		}


		////////////////

		public static void AddWidgetVisibilityHook( Func<string, bool> hook ) {
			var mymod = ModContent.GetInstance<HUDElementsLibMod>();

			mymod.VisibilityHooks.Add( hook );
		}
	}
}