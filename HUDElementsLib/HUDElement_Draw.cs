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

			if( this.IsInteractive() ) {
				this.DrawOverlaysIf( sb );
			}
		}

		private void DrawOverlaysIf( SpriteBatch sb ) {
			bool isHoverCollision = false;
			bool isHoverAnchorRight = false;
			bool isHoverAnchorBottom = false;

			if( Main.playerInventory ) {
				bool isAlt = Main.keyState.IsKeyDown( Keys.LeftAlt ) || Main.keyState.IsKeyDown( Keys.RightAlt );
				if( isAlt ) {
					this.DrawOverlays( sb, ref isHoverCollision, ref isHoverAnchorRight, ref isHoverAnchorBottom );
				}
			}

			string hoverText = this.GetHoverText( isHoverCollision, isHoverAnchorRight, isHoverAnchorBottom );
			if( !string.IsNullOrEmpty(hoverText) ) {
				this.DrawHoverTextIf( sb, hoverText );
			}
		}


		////

		private void DrawOverlays(
					SpriteBatch sb,
					ref bool isHoverCollision,
					ref bool isHoverAnchorRight,
					ref bool isHoverAnchorBottom ) {
			isHoverCollision = this.IsMouseHovering_Custom && this.CanToggleCollisionsViaControl();
			isHoverAnchorRight = this.IsMouseHovering_Custom && !this.IsAnchorLocked();
			isHoverAnchorBottom = this.IsMouseHovering_Custom && !this.IsAnchorLocked();

			//

			Rectangle area = this.GetHUDComputedArea( false );
			Color baseColor = this.IsDragLocked()
				? Color.Red
				: Color.White;
			baseColor *= this.IsMouseHovering_Custom
				? 1f
				: 0.8f;
			float brightness = this.IsDraggingSinceLastTick
				? 1f
				: 0.5f;

			//

			HUDElement.DrawBox( sb, area, baseColor * brightness, !this.IsMouseHovering_Custom );

			HUDElement.DrawOverlayControlsIf(
				sb: sb,
				area: area,
				brightness: brightness,
				collisionToggler: isHoverCollision
					? !this.IsIgnoringCollisions
					: (bool?)null,
				anchorRightButton: isHoverAnchorRight
					? this.IsRightAnchored()
					: (bool?)null,
				anchorBottomButton: isHoverAnchorBottom
					? this.IsBottomAnchored()
					: (bool?)null,
				hoverPoint: Main.MouseScreen,
				isHoverCollisionToggle: ref isHoverCollision,
				isHoverAnchorRightToggle: ref isHoverAnchorRight,
				isHoverAnchorBottomToggle: ref isHoverAnchorBottom
			);
			
			if( this.DisplacedPosition.HasValue ) {
				Color displacedColor = Color.Yellow * 0.5f;
				displacedColor *= this.IsMouseHovering_Custom
					? 1f
					: 0.65f;

				Rectangle displacedArea = this.GetHUDComputedArea( true );

				HUDElement.DrawBox( sb, displacedArea, displacedColor, !this.IsMouseHovering_Custom );
			}
		}


		////////////////

		private void DrawHoverTextIf( SpriteBatch sb, string text ) {
			if( !this.IsMouseHovering_Custom ) {
				return;
			}
			if( this.IsInteractingAny ) {
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