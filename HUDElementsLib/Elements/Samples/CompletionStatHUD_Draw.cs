using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;


namespace HUDElementsLib.Elements.Samples {
	public partial class CompletionStatHUD : HUDElement {
		protected override void PostDrawSelf( bool isSelfDrawn, SpriteBatch sb ) {
			if( !isSelfDrawn ) {
				return;
			}

			//

			string titleText = this.TitleText + ":";

			//

			DynamicSpriteFont font = Main.fontMouseText;

			Vector2 elemDim = new Vector2( this.Width.Pixels, this.Height.Pixels );
			Vector2 elemCenter = elemDim * 0.5f;
			Vector2 elemPos = this.GetHUDComputedPosition( true );

			//

			Vector2 titleDim = font.MeasureString( titleText );
			Vector2 titleCenter = titleDim * 0.5f;
			Vector2 titlePos = new Vector2(
				elemPos.X + (elemDim.X * 0.5f) - (titleDim.X * 0.5f),
				elemPos.Y
			);

			Utils.DrawBorderString(
				sb: sb,
				text: titleText,
				pos: titlePos,
				color: Color.White,
				scale: 1f
			);

			//

			(int completed, int total) = this.Stat.Invoke();
			(string statText, Color statColor) = CompletionStatHUD.RenderStat( completed, total );

			Vector2 statDim = font.MeasureString( statText );
			Vector2 statPos = new Vector2(
				elemPos.X + (elemDim.X * 0.5f) - (statDim.X * 0.5f),
				elemPos.Y + titleDim.Y + 4f
			);

			Utils.DrawBorderString(
				sb: sb,
				text: statText,
				pos: statPos,
				color: statColor,
				scale: 1f
			);
		}
	}
}