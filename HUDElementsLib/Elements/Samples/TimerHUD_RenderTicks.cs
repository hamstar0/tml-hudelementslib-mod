using System;
using Microsoft.Xna.Framework;
using Terraria;


namespace HUDElementsLib.Elements.Samples {
	public partial class TimerHUD : HUDElement {
		public static long DefaultCountdownTicker( long currentTicks, out Color color ) {
			if( currentTicks <= 1 ) {
				color = Color.DarkGray;

				return 0;
			}

			//

			currentTicks--;

			if( currentTicks > 60 * 60 ) {
				color = new Color( 128, 255, 128 );
			} else if( currentTicks > 30 * 60 ) {
				color = Color.White;
			} else if( currentTicks > 15 * 60 ) {
				color = new Color( 255, 255, 96 );
			} else if( currentTicks > 5 * 60 ) {
				color = new Color( 255, 96, 96 );
			} else {
				if( (currentTicks % 2) == 0 ) {
					color = new Color( 255, 96, 96 );
				} else {
					color = new Color( 255, 255, 64 );
				}
			}
			return currentTicks;
		}

		public static long DefaultClockTicker( long currentTicks, out Color color ) {
			color = Color.White;
			return currentTicks + 1;
		}


		////////////////

		public static (string, Color) RenderTimer( long ticks, bool showTicks, TimeTicker ticker ) {
			long absTicks = Math.Abs( ticks );
			long absSeconds = absTicks / 60L;
			long absMinutes = absSeconds / 60L;
			long absHours = absMinutes / 60L;

			string output = ticks < 0L
				? "-"
				: "";

			if( absHours > 0L ) {
				output += absHours;

				output += ":";
			}

			long absMinutesPer = absMinutes % 60L;
			if( absMinutesPer < 10L ) {
				output += "0" + absMinutesPer;
			} else {
				output += absMinutesPer;
			}

			output += ":";

			long absSecondsPer = absSeconds % 60L;
			if( absSecondsPer < 10L ) {
				output += "0" + absSecondsPer;
			} else {
				output += absSecondsPer;
			}

			if( showTicks ) {
				output += ":";

				long absTicksPer = absTicks % 60L;
				if( absTicksPer < 10L ) {
					output += "0" + absTicksPer;
				} else {
					output += absTicksPer;
				}
			}

			//

			Color color;
			long newTicks = ticker.Invoke( ticks, out _ );

			long tickDiff = ticks - newTicks;
			ticker.Invoke( ticks+tickDiff, out color );

			return (output, color);
		}
	}
}