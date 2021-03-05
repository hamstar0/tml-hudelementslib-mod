using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public override void Draw( SpriteBatch sb ) {
			base.Draw( sb );

			if( HUDElementsLibAPI.GetDraggingElement() != null ) {
				Rectangle area = this.GetRect();
				float tint = (float)Main.mouseTextColor / 255f;

				Utils.DrawRectangle(
					sb,
					area.TopLeft() + Main.screenPosition,
					area.BottomRight() + Main.screenPosition,
					Color.White * tint * 0.5f,
					Color.White * tint * 0.5f,
					2
				);
			}

			if( this.IsHovering && !this.IsDragging ) {
				Utils.DrawBorderStringFourWay(
					sb: sb,
					font: Main.fontMouseText,
					text: "Alt+Click to drag",
					x: Main.MouseScreen.X + 12,
					y: Main.MouseScreen.Y + 16,
					textColor: new Color( Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor ),
					borderColor: Color.Black,
					origin: default
				);
			}
		}
	}
}