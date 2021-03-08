using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public override void Draw( SpriteBatch sb ) {
			if( !this.IsEnabled() ) {
				return;
			}

			base.Draw( sb );

			if( Main.playerInventory ) {
				bool isAlt = Main.keyState.IsKeyDown( Keys.LeftAlt ) || Main.keyState.IsKeyDown( Keys.RightAlt );

				if( isAlt ) {
					Rectangle area = this.GetAreaOnHUD( false );
					Color baseColor = this.IsLocked()
						? Color.Red
						: Color.White;
					float pulse = (float)Main.mouseTextColor / 255f;
					float tint = this.IsDragging ? 1f : 0.5f;

					sb.Draw(
						texture: Main.magicPixel,
						destinationRectangle: area,
						color: baseColor * pulse * tint * 0.25f
					);

					Utils.DrawRectangle(
						sb: sb,
						start: area.TopLeft() + Main.screenPosition,
						end: area.BottomRight() + Main.screenPosition,
						colorStart: baseColor * pulse * tint * 0.5f,
						colorEnd: baseColor * pulse * tint * 0.5f,
						width: 2
					);
				}
			}

			if( !this.IsLocked() && this.IsHovering && !this.IsDragging ) {
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