using System;
using Microsoft.Xna.Framework;
using Terraria;


namespace HUDElementsLib.Elements.Samples {
	public partial class CompletionStatHUD : HUDElement {
		public static (string, Color) RenderStat( int completed, int total ) {
			string output = completed+" / "+total;
			Color color;

			float percent = (float)completed / (float)total;

			//

			if( percent >= 1f ) {
				color = Color.White;
			} else if( percent >= 0.75f ) {
				color = new Color( 128, 255, 255 );
			} else if( percent >= 0.5f ) {
				color = new Color( 128, 255, 128 );
			} else if( percent >= 0.3f ) {
				color = new Color( 255, 255, 96 );
			} else if( percent >= 0.15f ) {
				color = new Color( 255, 96, 96 );
			} else {
				color = new Color( 96, 96, 96 );
			}

			return (output, color);
		}
	}
}