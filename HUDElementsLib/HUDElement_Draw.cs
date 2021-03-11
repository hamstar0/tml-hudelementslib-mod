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

			this.DrawOverlays( sb );
		}

		private void DrawOverlays( SpriteBatch sb ) {
			string hoverText = this.IsLocked()
				? ""
				: "Alt+Click to drag";

			if( Main.playerInventory ) {
				bool isAlt = Main.keyState.IsKeyDown( Keys.LeftAlt ) || Main.keyState.IsKeyDown( Keys.RightAlt );
				if( isAlt ) {
					this.DrawBoxes( sb, ref hoverText );
				}
			}

			if( !string.IsNullOrEmpty(hoverText) ) {
				this.DrawHoverTextIf( sb, hoverText );
			}
		}


		////

		private void DrawBoxes( SpriteBatch sb, ref string hoverText ) {
			Rectangle area = this.GetHUDComputedArea( false );
			Color baseColor = this.IsLocked()
				? Color.Red
				: Color.White;
			float tint = this.IsDragging ? 1f : 0.5f;

			HUDElement.DrawFullBox(
				sb: sb,
				area: area,
				baseColor: baseColor,
				brightness: tint,
				collisionToggler: this.IsHovering && this.CanToggleCollisionsViaControl()
					? !this.IsIgnoringCollisions
					: (bool?)null,
				anchorRightButton: this.IsHovering && !this.IsLocked()
					? this.IsRightAnchored()
					: (bool?)null,
				anchorBottomButton: this.IsHovering && !this.IsLocked()
					? this.IsBottomAnchored()
					: (bool?)null,
				hoverPoint: Main.MouseScreen,
				hoverText: ref hoverText
			);
			
			if( this.DisplacedPosition.HasValue ) {
				Rectangle displacedArea = this.GetHUDComputedArea( true );

				HUDElement.DrawBox( sb, displacedArea, Color.Yellow, tint * 0.5f );
			}
		}


		////////////////

		private void DrawHoverTextIf( SpriteBatch sb, string text ) {
			if( !this.IsHovering ) {
				return;
			}
			if( this.IsDragging ) {
				return;
			}
			if( this.IsPressingControl ) {
				return;
			}

			Utils.DrawBorderStringFourWay(
				sb: sb,
				font: Main.fontMouseText,
				text: text,
				x: Main.MouseScreen.X + 12,
				y: Main.MouseScreen.Y + 16,
				textColor: new Color( Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor ),
				borderColor: Color.Black,
				origin: default
			);
		}
	}
}