using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public sealed override void Draw( SpriteBatch sb ) {
			if( !this.IsEnabled() ) {
				return;
			}

			base.Draw( sb );

			this.DrawOverlaysIf( sb );
		}

		////

		private void DrawOverlaysIf( SpriteBatch sb ) {
			bool isHoverCollision = false;
			bool isHoverAnchorRight = false;
			bool isHoverAnchorBottom = false;

			//if( Main.playerInventory ) {
			//bool mode = Main.keyState.IsKeyDown( Keys.LeftAlt )
			//	|| Main.keyState.IsKeyDown( Keys.RightAlt );
			bool editMode = HUDElementsLibMod.Instance.HUDEditMode.Current;

			if( editMode ) {
				this.DrawOverlayTitle( sb );
				this.DrawOverlayOfBoxes( sb );
				this.DrawOverlayOfControls( sb, out isHoverCollision, out isHoverAnchorRight, out isHoverAnchorBottom );
			}

			(string text, int duration) hoverInfo = this.GetHoverText(
				editMode,
				isHoverCollision,
				isHoverAnchorRight,
				isHoverAnchorBottom
			);

			if( !string.IsNullOrEmpty(hoverInfo.text ) ) {
				this.DrawHoverTextIf( sb, hoverInfo.text, hoverInfo.duration );
			} else {
				this.ClearHoverText();
			}
		}


		////////////////
		
		private void DrawOverlayTitle( SpriteBatch sb ) {
			Rectangle area = this.GetHUDComputedArea( false );

			sb.DrawString(
				spriteFont: Main.fontMouseText,
				text: this.Name,
				position: new Vector2( area.X+4, area.Y+8 ),
				color: Color.White,
				rotation: 0f,
				origin: default,
				scale: 0.75f,
				effects: SpriteEffects.None,
				layerDepth: 0
			);
		}


		////////////////

		private string _CurrentHoverText;
		private int _HoverTextDuration = -1;
		private int _HoverTextMaxDuration = -1;

		private void DrawHoverTextIf( SpriteBatch sb, string text, int duration ) {
			if( !this.IsMouseHovering_Custom ) {
				return;
			}
			if( this.IsInteractingAny ) {
				return;
			}

			float percent = 1f;
			Color textColor = new Color( Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor );

			if( this._CurrentHoverText != text ) {
				this._CurrentHoverText = text;
				this._HoverTextDuration = duration;
				this._HoverTextMaxDuration = duration;
			}

			if( this._HoverTextDuration > 0 ) {
				percent = (float)this._HoverTextDuration / (float)this._HoverTextMaxDuration;
				textColor = Color.White * percent;

				this._HoverTextDuration--;
			} else if( this._HoverTextDuration == 0 ) {
				return;
			}

			Vector2 dim = Main.fontMouseText.MeasureString( text );
			float x = Main.MouseScreen.X + 12;
			float y = Main.MouseScreen.Y + 16;

			if( (x+dim.X) >= Main.screenWidth ) {
				x = Main.screenWidth - dim.X;
			}
			if( (y+dim.Y) >= Main.screenHeight ) {
				y = Main.screenHeight - dim.Y;
			}

			Utils.DrawBorderStringFourWay(
				sb: sb,
				font: Main.fontMouseText,
				text: text,
				x: x,
				y: y,
				textColor: textColor,
				borderColor: Color.Black * percent,
				origin: default
			);
		}


		private void ClearHoverText() {
			this._CurrentHoverText = "";
		}
	}
}