using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public static void DrawFullBox(
					SpriteBatch sb,
					Rectangle area,
					Color baseColor,
					float brightness,
					bool? collisionToggler,
					bool? anchorRightButton,
					bool? anchorBottomButton,
					Vector2 hoverPoint,
					ref string hoverText ) {
			HUDElement.DrawBox( sb, area, baseColor, brightness );

			if( area.Contains(hoverPoint.ToPoint()) ) {
				if( collisionToggler.HasValue ) {
					HUDElement.DrawBoxCollisionToggler(
						sb: sb,
						area: area,
						brightness: brightness,
						on: collisionToggler.Value,
						hoverPoint: hoverPoint,
						hoverText: ref hoverText
					);
				}

				if( anchorRightButton.HasValue && anchorBottomButton.HasValue ) {
					HUDElement.DrawBoxAnchorButtons(
						sb: sb,
						area: area,
						brightness: brightness,
						onRight: anchorRightButton.Value,
						onBottom: anchorBottomButton.Value,
						hoverPoint: hoverPoint,
						ref hoverText
					);
				}
			}
		}


		////////////////

		public static void DrawBox(
					SpriteBatch sb,
					Rectangle area,
					Color baseColor,
					float brightness ) {
			float pulse = (float)Main.mouseTextColor / 255f;

			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: area,
				color: baseColor * pulse * brightness * 0.25f
			);

			Utils.DrawRectangle(
				sb: sb,
				start: area.TopLeft() + Main.screenPosition,
				end: area.BottomRight() + Main.screenPosition,
				colorStart: baseColor * pulse * brightness * 0.5f,
				colorEnd: baseColor * pulse * brightness * 0.5f,
				width: 2
			);
		}
	}
}