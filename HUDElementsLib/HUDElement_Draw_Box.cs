using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public static void DrawBox(
					SpriteBatch sb,
					Rectangle area,
					Color baseColor,
					bool pulses ) {
			float brightness = 1f;

			if( pulses ) {
				brightness *= (float)Main.mouseTextColor / 255f;
			}

			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: area,
				color: baseColor * brightness * 0.5f
			);

			Utils.DrawRectangle(
				sb: sb,
				start: area.TopLeft() + Main.screenPosition,
				end: area.BottomRight() + Main.screenPosition,
				colorStart: baseColor * brightness * 1f,
				colorEnd: baseColor * brightness * 1f,
				width: 2
			);
		}
	}
}