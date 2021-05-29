using System;
using Microsoft.Xna.Framework;
using Terraria;


namespace HUDElementsLib.Libraries.Libraries.HUD {
	/// <summary>
	/// Assorted static library functions pertaining to in-game HUD elements. 
	/// </summary>
	public class HUDElementLibraries {
		/// <summary>
		/// Top left screen position of player's health.
		/// </summary>
		/// <returns></returns>
		public static Vector2 GetVanillaHealthDisplayScreenPosition() =>
			new Vector2( Main.screenWidth - 300, 86 );


		/// <summary>
		/// Top left screen position of player's accessories (`player.armor[3+]`).
		/// </summary>
		/// <param name="slot">Accessory (not `armor` index) slot number.</param>
		/// <returns></returns>
		public static Vector2 GetVanillaAccessorySlotScreenPosition( int slot ) {
			/*int mapOffsetY = 0;
			if( Main.mapEnabled ) {
				if( !Main.mapFullscreen && Main.mapStyle == 1 ) {
					mapOffsetY = 256;
				}
			}

			if( (mapOffsetY + Main.instance.RecommendedEquipmentAreaPushUp) > Main.screenHeight ) {
				mapOffsetY = Main.screenHeight - Main.instance.RecommendedEquipmentAreaPushUp;
			}

			int x = Main.screenWidth - 64 - 28;
			int y = 178 + mapOffsetY;
			y += slot * 56;

			return new Vector2( x, y );*/

			var pos = new Vector2(
				Main.screenWidth - 92,
				318 + (48 * slot)
			);

			if( Main.mapStyle == 1 ) {
				int mapOffsetY = Main.screenHeight - Main.instance.RecommendedEquipmentAreaPushUp;  //600
				if( mapOffsetY > 256 ) {
					mapOffsetY = 255;
				}
//DebugHelpers.Print( "acc", "pos: "+(int)pos.Y+", sh: "+Main.screenHeight+", re: "+Main.instance.RecommendedEquipmentAreaPushUp );
				pos.Y += mapOffsetY;
			}

			return pos;
		}
	}
}
