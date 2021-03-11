using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public static void DrawOverlayControlsIf(
					SpriteBatch sb,
					Rectangle area,
					Color baseColor,
					float brightness,
					bool? collisionToggler,
					bool? anchorRightButton,
					bool? anchorBottomButton,
					Vector2 hoverPoint,
					ref bool isHoverCollisionToggle,
					ref bool isHoverAnchorRightToggle,
					ref bool isHoverAnchorBottomToggle ) {
			if( !area.Contains( hoverPoint.ToPoint() ) ) {
				return;
			}

			if( collisionToggler.HasValue ) {
				HUDElement.DrawBoxCollisionToggler(
					sb: sb,
					area: area,
					brightness: brightness,
					on: collisionToggler.Value,
					hoverPoint: hoverPoint,
					isHovering: ref isHoverCollisionToggle
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
					isHoverRight: ref isHoverAnchorRightToggle,
					isHoverBottom: ref isHoverAnchorBottomToggle
				);
			}
		}


		////////////////

		public static void DrawBoxCollisionToggler(
					SpriteBatch sb,
					Rectangle area,
					float brightness,
					bool on,
					Vector2 hoverPoint,
					ref bool isHovering ) {
			var buttonArea = HUDElement.GetCollisionTogglerForBox( area );
			var buttonIconArea = HUDElement.GetCollisionTogglerIconForBox( area );

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
				texture: Main.itemTexture[ItemID.Actuator],
				destinationRectangle: buttonIconArea,
				color: iconColor
			);
		}


		public static void DrawBoxAnchorButtons(
					SpriteBatch sb,
					Rectangle area,
					float brightness,
					bool onRight,
					bool onBottom,
					Vector2 hoverPoint,
					ref bool isHoverRight,
					ref bool isHoverBottom ) {
			Rectangle rArea = HUDElement.GetRightAnchorButtonForBox( area );
			Rectangle bArea = HUDElement.GetBottomAnchorButtonForBox( area );
			Rectangle iconBgArea = HUDElement.GetAnchorButtonIconBgForBox( area );
			Rectangle iconArea = HUDElement.GetAnchorButtonIconForBox( area );

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