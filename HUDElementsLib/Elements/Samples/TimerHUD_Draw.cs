using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;


namespace HUDElementsLib.Elements.Samples {
	public partial class TimerHUD : HUDElement {
		protected override void PostDrawSelf( bool isSelfDrawn, SpriteBatch sb ) {
			if( !isSelfDrawn ) {
				return;
			}

			//

			DynamicSpriteFont font = Main.fontMouseText;

			//CalculatedStyle dim = this.GetOuterDimensions();
			Vector2 elemDim = new Vector2( this.Width.Pixels, this.Height.Pixels );
			Vector2 elemCenter = elemDim * 0.5f;
			Vector2 elemPos = this.GetHUDComputedPosition( true );	//new Vector2( dim.X, dim.Y )

			//

			Vector2 titleDim = font.MeasureString( this.TitleText );
			Vector2 titleCenter = titleDim * 0.5f;
			Vector2 titlePos = new Vector2(
				elemPos.X + (elemDim.X * 0.5f) - (titleDim.X * 0.5f),
				elemPos.Y
			);
			//Vector2 titleMidPos = new Vector2(
			//	elemPos.X + elemCenter.X,
			//	elemPos.Y + titleCenter.Y
			//);

			Utils.DrawBorderString(
				sb: sb,
				text: this.TitleText,
				pos: titlePos,
				color: Color.White,
				scale: 1f
			);
			//sb.DrawString(
			//	spriteFont: font,
			//	text: this.TitleText,
			//	position: titleMidPos,
			//	color: Color.White,
			//	rotation: 0f,
			//	origin: titleCenter,
			//	scale: 1f,
			//	effects: SpriteEffects.None,
			//	layerDepth: 0f
			//);

			//

			(string timerText, Color timerColor) = TimerHUD.RenderTimer(
				this.CurrentTicks,
				this.ShowTicks,
				this.Ticker
			);

			Vector2 timerDim = font.MeasureString( timerText );
			//Vector2 timerCenter = timerDim * 0.5f;
			Vector2 timerPos = new Vector2(
				elemPos.X + (elemDim.X * 0.5f) - (timerDim.X * 0.5f),
				elemPos.Y + titleDim.Y + 4f
			);
			//Vector2 timerMidPos = new Vector2(
			//	elemPos.X + elemCenter.X,
			//	elemPos.Y + titleDim.Y + timerCenter.Y + 4f
			//);

			Utils.DrawBorderString(
				sb: sb,
				text: timerText,
				pos: timerPos,
				color: timerColor,
				scale: 1f
			);
			//sb.DrawString(
			//	spriteFont: font,
			//	text: timerText,
			//	position: timerMidPos,
			//	color: timerColor,
			//	rotation: 0f,
			//	origin: timerCenter,
			//	scale: 1f,
			//	effects: SpriteEffects.None,
			//	layerDepth: 0f
			//);
		}
	}
}