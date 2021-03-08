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
					float tint = (float)Main.mouseTextColor / 255f;
					float dragTint = tint * (this.IsDragging ? 0.75f : 0.35f);

					if( this.IsLocked() ) {
						sb.Draw(
							texture: Main.magicPixel,
							destinationRectangle: area,
							color: Color.Red * 0.15f
						);
					} else {
						sb.Draw(
							texture: Main.magicPixel,
							destinationRectangle: area,
							color: Color.White * dragTint * 0.5f
						);
					}

					Utils.DrawRectangle(
						sb,
						area.TopLeft() + Main.screenPosition,
						area.BottomRight() + Main.screenPosition,
						Color.White * dragTint,
						Color.White * dragTint,
						this.IsDragging ? 3 : 2
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