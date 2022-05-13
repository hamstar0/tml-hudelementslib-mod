using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private void DrawEditModeControls_If(
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

			Rectangle realArea = this.GetHUDComputedArea( false );
			float brightness = this.IsDraggingSinceLastTick
				? 1f
				: 0.5f;
/*ModLibsCore.Libraries.Debug.DebugLibraries.Print(
	"edit_ctrls_1_"+realArea+" "+this.Name,
	", imh:"+this.IsMouseHoveringEditableBox
	+", hc:"+this.IsCollisionToggleable()
	+", hr:"+this.IsDragLocked()
	+", har:"+this.IsAnchorsToggleable()
	+", hab:"+this.IsAnchorsToggleable()
);*/
			//

			HUDElement.DrawEditModeControls_If(
				sb: sb,
				realArea: realArea,
				brightness: brightness,
				collisionToggler: isHoverCollision
					? !this.IsIgnoringCollisions
					: (bool?)null,
				resetButton: isHoverReset
					? !this.IsDragLocked()
					: (bool?)null,
				anchorRightButton: this.CurrentPositionPercent.X >= 1f
					? true
					: (bool?)null,
				anchorBottomButton: this.CurrentPositionPercent.Y >= 1f
					? true
					: (bool?)null,
				hoverPoint: Main.MouseScreen,
				isHoverCollisionToggle: ref isHoverCollision,
				isHoverResetButton: ref isHoverReset,
				isHoverAnchorRightToggle: ref isHoverAnchorRight,
				isHoverAnchorBottomToggle: ref isHoverAnchorBottom
			);
		}


		////

		public static void DrawEditModeControls_If(
					SpriteBatch sb,
					Rectangle realArea,
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
			if( !realArea.Contains( hoverPoint.ToPoint() ) ) {
				return;
			}

/*ModLibsCore.Libraries.Debug.DebugLibraries.Print(
	"edit_ctrls_2_"+realArea.ToString(),
	"b:"+brightness
	+", ct:"+collisionToggler
	+", ar:"+resetButton
	+", ab:"+anchorBottomButton
	+", hct:"+isHoverCollisionToggle
	+", hrb:"+isHoverResetButton
	+", har:"+isHoverAnchorRightToggle
	+", hab:"+isHoverAnchorBottomToggle
);*/
			if( collisionToggler.HasValue ) {
				HUDElement.DrawEditModeControls_CollisionToggler_If(
					sb: sb,
					area: realArea,
					brightness: brightness,
					on: collisionToggler.Value,
					hoverPoint: hoverPoint,
					isHovering: ref isHoverCollisionToggle
				);
			}

			if( resetButton.HasValue ) {
				HUDElement.DrawEditModeControls_ResetButton_If(
					sb: sb,
					area: realArea,
					brightness: brightness,
					on: resetButton.Value,
					hoverPoint: hoverPoint,
					isHovering: ref isHoverResetButton
				);
			}

			if( anchorRightButton.HasValue && anchorBottomButton.HasValue ) {
				HUDElement.DrawEditModeControls_AnchorButtons_If(
					sb: sb,
					area: realArea,
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