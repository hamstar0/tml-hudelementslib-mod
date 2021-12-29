using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private void DrawEditModeControls(
					SpriteBatch sb,
					out bool isHoverCollision,
					out bool isHoverReset,
					out bool isHoverAnchorRight,
					out bool isHoverAnchorBottom ) {
			isHoverCollision = this.IsMouseHoveringEditableBox && this.IsCollisionToggleable();
			isHoverReset = this.IsMouseHoveringEditableBox && !this.IsDragLocked();
			isHoverAnchorRight = this.IsMouseHoveringEditableBox && this.IsAnchorsToggleable();
			isHoverAnchorBottom = this.IsMouseHoveringEditableBox && this.IsAnchorsToggleable();

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
				resetButton: isHoverReset
					? !this.IsDragLocked()
					: (bool?)null,
				anchorRightButton: isHoverAnchorRight
					? this.IsRightAnchored()
					: (bool?)null,
				anchorBottomButton: isHoverAnchorBottom
					? this.IsBottomAnchored()
					: (bool?)null,
				hoverPoint: Main.MouseScreen,
				isHoverCollisionToggle: ref isHoverCollision,
				isHoverResetButton: ref isHoverReset,
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
					bool? resetButton,
					bool? anchorRightButton,
					bool? anchorBottomButton,
					Vector2 hoverPoint,
					ref bool isHoverCollisionToggle,
					ref bool isHoverResetButton,
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

			if( resetButton.HasValue ) {
				HUDElement.DrawControlsResetButtonIf(
					sb: sb,
					area: area,
					brightness: brightness,
					on: resetButton.Value,
					hoverPoint: hoverPoint,
					isHovering: ref isHoverResetButton
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