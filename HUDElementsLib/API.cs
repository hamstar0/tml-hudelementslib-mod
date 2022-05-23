using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace HUDElementsLib {
	public static class HUDElementsLibAPI {
		private static void MessageAboutHUD_If() {
			Mod msgMod = ModLoader.GetMod( "Messages" );
			if( msgMod == null ) {
				return;
			}

			//
			
			void AddUsageMessage() {
				msgMod = ModLoader.GetMod( "Messages" );
				if( msgMod.Version < new Version(1, 4, 0, 3) ) {
					return;
				}

				//

				string id = "HUDElementsLibUsage";

				try {
					bool isUnread = (bool)msgMod.Call( "IsUnread", id );

					object rawParentMsg = msgMod.Call( "GetMessage", "Messages - Mod Info" ); //MessagesAPI.ModInfoCategoryMsg

					//

					Color? color = null;

					msgMod.Call(
						"AddColoredMessage",
						"Reposition HUD elements via. hotkey", //title:
						"Bind a key to activate Edit Mode to reposition custom HUD elements freely to your liking.",    //description:
						(Color?)color,	//color:
						(Mod)HUDElementsLibMod.Instance, //modOfOrigin:
						(bool)isUnread,    //alertPlayer:
						false,  //isImportant:
						rawParentMsg, //parentMessage:
						(string)id, //id:
						0   //weight:
					);
				} catch( Exception e ) {
					HUDElementsLibMod.Instance.Logger.Error( "", e );
				}
			}

			Action usageMessageAdder = AddUsageMessage;

			//

			msgMod.Call( "AddMessagesCategoriesInitializeEvent", usageMessageAdder );
		}


		////////////////

		public static bool IsEditModeActive() {
			return HUDElementsLibMod.Instance
				.HUDEditMode?
				.Current
					?? false;
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