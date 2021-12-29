using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public static void DrawEditModeBox(
					SpriteBatch sb,
					Rectangle area,
					Color color,
					float bodyOpacity,
					bool pulses ) {
			float brightness = 1f;

			if( pulses ) {
				brightness *= (float)Main.mouseTextColor / 255f;
			}

			sb.Draw(
				texture: Main.magicPixel,
				destinationRectangle: area,
				color: color * bodyOpacity * brightness
			);

			Utils.DrawRectangle(
				sb: sb,
				start: area.TopLeft() + Main.screenPosition,
				end: area.BottomRight() + Main.screenPosition,
				colorStart: color * brightness,
				colorEnd: color * brightness,
				width: 2
			);
		}


		////////////////

		private void DrawEditModeBoxes( SpriteBatch sb ) {
			Rectangle area = this.GetHUDComputedArea( false );
			Color baseColor = this.IsDragLocked()
				? Color.Red
				: Color.White;
			baseColor *= this.IsMouseHoveringEditableBox
				? 1f
				: 0.8f;
			float brightness = this.IsDraggingSinceLastTick
				? 1f
				: 0.5f;

			//

			HUDElement.DrawEditModeBox(
				sb: sb,
				area: area,
				color: baseColor * brightness,
				bodyOpacity: this.IsIgnoringCollisions ? 0.2f : 0.5f,
				pulses: !this.IsMouseHoveringEditableBox
			);

			if( this.DisplacedPosition.HasValue ) {
				Color displacedColor = Color.Yellow * 0.5f;
				displacedColor *= this.IsMouseHoveringEditableBox
					? 1f
					: 0.65f;

				Rectangle displacedArea = this.GetHUDComputedArea( true );

				HUDElement.DrawEditModeBox(
					sb: sb,
					area: displacedArea,
					color: displacedColor,
					bodyOpacity: 0.5f,
					pulses: !this.IsMouseHoveringEditableBox
				);
			}
		}
	}
}