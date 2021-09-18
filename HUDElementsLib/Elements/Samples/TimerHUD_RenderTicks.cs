using System;
using Microsoft.Xna.Framework;
using Terraria;


namespace HUDElementsLib.Elements.Samples {
	public partial class TimerHUD : HUDElement {
		public static (string, Color) RenderTimer( long ticks, bool showTicks, TimeTicker ticker ) {
			string output = ticks < 0L
				? "-"
				: "";

			long absTicks = Math.Abs( ticks );
			long absSeconds = absTicks / 60L;
			long absMinutes = absSeconds / 60L;
			long absHours = absMinutes / 60L;

			if( absHours > 0L ) {
				output = output + absHours + ":";
			}

			if( absMinutes > 10L ) {
				output = output + "0" + absMinutes;
			} else {
				output = output + absMinutes;
			}

			output = output + ":" + absSeconds;

			if( showTicks ) {
				long absTicksOnly = absTicks % 60L;

				if( absTicksOnly > 10L ) {
					output = output + ":0" + absTicksOnly;
				} else {
					output = output + ":" + absTicksOnly;
				}
			}

			ticker.Invoke( ticks+1L, out Color color );

			return (output, color);
		}
	}
}