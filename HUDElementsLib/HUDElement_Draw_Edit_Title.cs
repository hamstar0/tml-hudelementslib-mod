using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private void DrawEditModeTitle( SpriteBatch sb ) {
			Rectangle area = this.GetHUDComputedArea( false );

			sb.DrawString(
				spriteFont: Main.fontMouseText,
				text: this.Name,
				position: new Vector2( area.X+4, area.Y+8 ),
				color: Color.White,
				rotation: 0f,
				origin: default,
				scale: 0.75f,
				effects: SpriteEffects.None,
				layerDepth: 0
			);
		}
	}
}