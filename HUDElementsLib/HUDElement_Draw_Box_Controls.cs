using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public static void DrawBoxCollisionToggler(
					SpriteBatch sb,
					Rectangle area,
					float brightness,
					bool on,
					Vector2 hoverPoint,
					ref string hoverText ) {
			var buttonArea = HUDElement.GetCollisionTogglerForBox( area );
			var buttonIconArea = HUDElement.GetCollisionTogglerIconForBox( area );
			bool isHovering = buttonArea.Contains( hoverPoint.ToPoint() );

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
				texture: Main.itemTexture[ItemID.Actuator],
				destinationRectangle: buttonIconArea,
				color: iconColor
			);

			if( isHovering ) {
				hoverText = "Toggle collisions";
			}
		}


		public static void DrawBoxAnchorButtons(
					SpriteBatch sb,
					Rectangle area,
					float brightness,
					bool onRight,
					bool onBottom,
					Vector2 hoverPoint,
					ref string hoverText ) {
			Rectangle rArea = HUDElement.GetRightAnchorButtonForBox( area );
			Rectangle bArea = HUDElement.GetBottomAnchorButtonForBox( area );
			Rectangle iconBgArea = HUDElement.GetAnchorButtonIconBgForBox( area );
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

			if( rHover ) {
				hoverText = "Anchor to right edge of screen";
			} else if( bHover ) {
				hoverText = "Anchor to bottom edge of screen";
			}
		}
	}
}