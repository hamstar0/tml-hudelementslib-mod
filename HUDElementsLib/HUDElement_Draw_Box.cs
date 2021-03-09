using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
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
					Vector2 hoverPoint ) {
			HUDElement.DrawBox( sb, area, baseColor, brightness );

			if( collisionToggler.HasValue ) {
				HUDElement.DrawBoxCollisionToggler( sb, area, brightness, collisionToggler.Value, hoverPoint );
			}

			if( anchorRightButton.HasValue && anchorBottomButton.HasValue ) {
				HUDElement.DrawBoxAnchorButtons(
					sb,
					area,
					brightness,
					anchorRightButton.Value,
					anchorBottomButton.Value,
					hoverPoint
				);
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


		////////////////

		public static void DrawBoxCollisionToggler(
					SpriteBatch sb,
					Rectangle area,
					float brightness,
					bool on,
					Vector2 hoverPoint ) {
			var buttonArea = HUDElement.GetCollisionTogglerForBox( area );

			Color iconColor = on
				? Color.White
				: Color.Lerp( Color.Red, Color.White, 0.25f );
			iconColor *= buttonArea.Contains(hoverPoint.ToPoint())
				? 1f
				: 0.75f;

			sb.Draw(
				texture: Main.itemTexture[ItemID.Actuator],
				destinationRectangle: buttonArea,
				color: iconColor * brightness
			);
		}


		public static void DrawBoxAnchorButtons(
					SpriteBatch sb,
					Rectangle area,
					float brightness,
					bool onRight,
					bool onBottom,
					Vector2 hoverPoint ) {
			Rectangle rArea = HUDElement.GetRightAnchorButtonForBox( area );
			Rectangle bArea = HUDElement.GetBottomAnchorButtonForBox( area );
			Rectangle iconArea = HUDElement.GetAnchorButtonIconForBox( area );
			bool rHover = rArea.Contains( hoverPoint.ToPoint() );
			bool bHover = bArea.Contains( hoverPoint.ToPoint() );

			Color rColor = onRight
				? Color.White
				: Color.White * (rHover ? 0.6f : 0.4f) * brightness;
			Color bColor = onBottom
				? Color.White
				: Color.White * (bHover ? 0.6f : 0.4f) * brightness;

			sb.Draw(
				texture: Main.itemTexture[ ItemID.WallAnchor ],
				destinationRectangle: iconArea,
				color: Color.White * brightness
			);

			//

			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: rArea,
				color: rColor
			);
			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: bArea,
				color: bColor
			);
		}
	}
}