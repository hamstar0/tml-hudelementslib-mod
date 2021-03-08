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
					this.DrawBoxes( sb );
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


		////

		private void DrawBoxes( SpriteBatch sb ) {
			Rectangle area = this.GetAreaOnHUD( true );
			Color baseColor = this.IsLocked()
				? Color.Red
				: Color.White;
			float tint = this.IsDragging ? 1f : 0.5f;

			HUDElement.DrawBox(
				sb: sb,
				area: area,
				baseColor: baseColor,
				tint: tint,
				collisionToggler: this.IsHovering// && this.CanToggleCollisions()
					? !this.IsIgnoringCollisions()
					: (bool?)null,
				anchorRightButton: this.IsHovering && !this.IsLocked()
					? this.IsRightAnchored()
					: (bool?)null,
				anchorBottomButton: this.IsHovering && !this.IsLocked()
					? this.IsBottomAnchored()
					: (bool?)null
			);

			if( this.DisplacedPosition.HasValue ) {
				Rectangle displacedArea = this.GetAreaOnHUD( false );

				HUDElement.DrawBox(
					sb: sb,
					area: displacedArea,
					baseColor: Color.Yellow,
					tint: tint * 0.5f,
					collisionToggler: (bool?)null,
					anchorRightButton: (bool?)null,
					anchorBottomButton: (bool?)null
				);
			}
		}
	}
}