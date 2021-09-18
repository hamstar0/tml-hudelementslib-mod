using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib.Elements.Samples {
	public partial class TimerHUD : HUDElement {
		protected override void DrawSelf( SpriteBatch sb ) {
			base.DrawSelf( sb );

			DynamicSpriteFont font = Main.fontMouseText;
			Vector2 textDim = font.MeasureString( this.TitleText );

			CalculatedStyle dim = this.GetDimensions();
			Vector2 dimCenter = dim.Center();
			var pos = new Vector2( dimCenter.X, textDim.Y * 0.5f );

			//

			sb.DrawString(
				spriteFont: font,
				text: this.TitleText,
				position: pos,
				color: Color.White,
				rotation: 0f,
				origin: textDim * 0.5f,
				scale: 1f,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);

			//

			(string timerText, Color timerColor) = TimerHUD.RenderTimer( this.CurrentTicks, this.ShowTicks, this.Ticker );

			sb.DrawString(
				spriteFont: font,
				text: timerText,
				position: new Vector2( pos.X, textDim.Y + 2f ),
				color: timerColor,
				rotation: 0f,
				origin: textDim * 0.5f,
				scale: 1f,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);
		}
	}
}