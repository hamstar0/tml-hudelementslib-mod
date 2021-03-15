using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public static void DrawControlsResetButtonIf(
					SpriteBatch sb,
					Rectangle area,
					float brightness,
					bool on,
					Vector2 hoverPoint,
					ref bool isHovering ) {
			if( !HUDElementsLibConfig.Instance.EnableResetButtonControl ) {
				return;
			}

			var buttonArea = HUDElement.GetResetButtonArea( area );
			var buttonIconArea = HUDElement.GetResetButtonIconArea( area );

			isHovering = buttonArea.Contains( hoverPoint.ToPoint() );

			Color bgColor = on
				? Color.White
				: Color.White * (isHovering ? 0.65f : 0.35f) * brightness;
			bgColor *= isHovering ? 1f : 0.75f;
			Color iconColor = on
				? Color.White
				: Color.Red;
			iconColor *= 0.8f;
			iconColor *= isHovering ? 1f : 0.85f;

			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: buttonArea,
				color: bgColor * brightness
			);
			sb.Draw(
				texture: Main.itemTexture[ItemID.MagicMirror],
				destinationRectangle: buttonIconArea,
				color: iconColor
			);
		}
	}
}