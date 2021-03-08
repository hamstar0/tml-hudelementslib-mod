using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public static void DrawBox(
					SpriteBatch sb,
					Rectangle area,
					Color baseColor,
					float tint,
					bool? collisionToggler,
					bool? anchorRightButton,
					bool? anchorBottomButton ) {
			float pulse = (float)Main.mouseTextColor / 255f;

			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: area,
				color: baseColor * pulse * tint * 0.25f
			);

			Utils.DrawRectangle(
				sb: sb,
				start: area.TopLeft() + Main.screenPosition,
				end: area.BottomRight() + Main.screenPosition,
				colorStart: baseColor * pulse * tint * 0.5f,
				colorEnd: baseColor * pulse * tint * 0.5f,
				width: 2
			);

			if( collisionToggler.HasValue ) {
				var buttonArea = new Rectangle( area.Right - 16, 0, 16, 16 );
				Color iconColor = collisionToggler.Value
					? Color.White
					: Color.Red;

				if( !buttonArea.Contains( Main.MouseScreen.ToPoint() ) ) {
					iconColor *= 0.75f;
				}

				sb.Draw(
					texture: Main.itemTexture[ItemID.Actuator],
					destinationRectangle: buttonArea,
					color: iconColor * tint
				);
			}

			if( anchorRightButton.HasValue || anchorBottomButton.HasValue ) {
				var buttonArea = new Rectangle( area.Right - 16, area.Bottom - 16, 16, 16 );

				sb.Draw(
					texture: Main.itemTexture[ItemID.WallAnchor],
					destinationRectangle: buttonArea,
					color: Color.White * tint * 0.5f
				);

				if( anchorRightButton.HasValue ) {
					Color iconColor = anchorRightButton.Value
						? Color.White
						: Color.White * 0.5f;

					sb.Draw(
						texture: Main.magicPixel,
						destinationRectangle: new Rectangle( area.Left, area.Bottom - 16, area.Width, 16 ),
						color: iconColor * tint
					);
				}
			}
		}
	}
}