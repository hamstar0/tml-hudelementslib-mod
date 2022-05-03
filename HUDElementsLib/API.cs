using System;
using Terraria;
using Terraria.ModLoader;


namespace HUDElementsLib {
	public static class HUDElementsLibAPI {
		private static void MessageAboutHUD_If() {
			Mod msgMod = ModLoader.GetMod( "Messages" );
			if( msgMod != null ) {
				return;
			}

			//
			
			void AddUsageMessage() {
				string id = "HUDElementsLibUsage";

				bool isUnread = (bool)msgMod.Call( "IsUnread", id );

				object rawParentMsg = msgMod.Call( "GetMessage", "Messages - Mod Info" ); //MessagesAPI.ModInfoCategoryMsg

				//

				msgMod.Call(
					"AddMessage",
					"Reposition HUD elements via. hotkey", //title:
					"Bind a key to activate Edit Mode to reposition custom HUD elements freely to your liking.",    //description:
					HUDElementsLibMod.Instance, //modOfOrigin:
					id, //id:
					isUnread,    //alertPlayer:
					false,  //isImportant:
					rawParentMsg //parentMessage:
				);
			}

			Action usageMessageAdder = AddUsageMessage;

			//

			msgMod.Call( "AddMessagesCategoriesInitializeEvent", usageMessageAdder );
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

			HUDElementsLibAPI.MessageAboutHUD_If();
		}


		////////////////

		public static void AddWidgetVisibilityHook( Func<string, bool> hook ) {
			var mymod = ModContent.GetInstance<HUDElementsLibMod>();

			mymod.VisibilityHooks.Add( hook );
		}
	}
}