using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public static void DrawControlsAnchorButtonsIf(
					SpriteBatch sb,
					Rectangle area,
					float brightness,
					bool onRight,
					bool onBottom,
					Vector2 hoverPoint,
					ref bool isHoverRight,
					ref bool isHoverBottom ) {
			if( !HUDElementsLibConfig.Instance.EnableAnchorsToggleControl ) {
				return;
			}

			Rectangle rArea = HUDElement.GetRightAnchorButtonArea( area );
			Rectangle bArea = HUDElement.GetBottomAnchorButtonArea( area );
			Rectangle iconBgArea = HUDElement.GetAnchorButtonIconBgArea( area );
			Rectangle iconArea = HUDElement.GetAnchorButtonIconArea( area );

			isHoverRight = rArea.Contains( hoverPoint.ToPoint() );
			isHoverBottom = bArea.Contains( hoverPoint.ToPoint() );

			Color rColor = onRight
				? Color.White
				: Color.White * (isHoverRight ? 0.6f : 0.4f) * brightness;
			Color bColor = onBottom
				? Color.White
				: Color.White * (isHoverBottom ? 0.6f : 0.4f) * brightness;

			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: iconBgArea,
				color: Color.White * brightness
			);
			sb.Draw(
				texture: Main.itemTexture[ ItemID.WallAnchor ],
				destinationRectangle: iconArea,
				color: Color.White
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