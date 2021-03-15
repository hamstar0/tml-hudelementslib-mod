using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private void DrawOverlayOfControls(
					SpriteBatch sb,
					out bool isHoverCollision,
					out bool isHoverAnchorRight,
					out bool isHoverAnchorBottom ) {
			isHoverCollision = this.IsMouseHovering_Custom && this.IsCollisionToggleable();
			isHoverAnchorRight = this.IsMouseHovering_Custom && this.IsAnchorsToggleable();
			isHoverAnchorBottom = this.IsMouseHovering_Custom && this.IsAnchorsToggleable();

			//

			Rectangle area = this.GetHUDComputedArea( false );
			float brightness = this.IsDraggingSinceLastTick
				? 1f
				: 0.5f;

			//

			HUDElement.DrawControlsIf(
				sb: sb,
				area: area,
				brightness: brightness,
				collisionToggler: isHoverCollision
					? !this.IsIgnoringCollisions
					: (bool?)null,
				anchorRightButton: isHoverAnchorRight
					? this.IsRightAnchored()
					: (bool?)null,
				anchorBottomButton: isHoverAnchorBottom
					? this.IsBottomAnchored()
					: (bool?)null,
				hoverPoint: Main.MouseScreen,
				isHoverCollisionToggle: ref isHoverCollision,
				isHoverAnchorRightToggle: ref isHoverAnchorRight,
				isHoverAnchorBottomToggle: ref isHoverAnchorBottom
			);
		}


		////

		public static void DrawControlsIf(
					SpriteBatch sb,
					Rectangle area,
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
				HUDElement.DrawControlsCollisionTogglerIf(
					sb: sb,
					area: area,
					brightness: brightness,
					on: collisionToggler.Value,
					hoverPoint: hoverPoint,
					isHovering: ref isHoverCollisionToggle
				);
			}

			if( anchorRightButton.HasValue && anchorBottomButton.HasValue ) {
				HUDElement.DrawControlsAnchorButtonsIf(
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
	}
}