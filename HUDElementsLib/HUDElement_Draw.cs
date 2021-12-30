using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public sealed override void Draw( SpriteBatch sb ) {
			if( !this.IsEnabled() ) {
				return;
			}

			//
			
			var mymod = HUDElementsLibMod.Instance;
			if( mymod.VisibilityHooks.Count > 0 ) {
				if( mymod.VisibilityHooks.Any( h => !h.Invoke(this.Name) ) ) {
					return;
				}
			}

			//
			
			base.Draw( sb );

			//

			this.DrawOverlays_If( sb );
		}


		////

		protected sealed override void DrawSelf( SpriteBatch sb ) {
			Vector2 elemPos = this.GetHUDComputedPosition( true );
			Vector2 dim = this.GetHUDComputedDimensions();

			this.Left.Set( elemPos.X, 0f );
			this.Top.Set( elemPos.Y, 0f );
			this.Width.Set( dim.X, 0f );
			this.Height.Set( dim.Y, 0f );

			//

			this.Recalculate();

			//

			bool isSelfDrawn = this.PreDrawSelf( sb );

			if( isSelfDrawn ) {
				base.DrawSelf( sb );
			}

			this.PostDrawSelf( isSelfDrawn, sb );
		}


		////

		protected virtual bool PreDrawSelf( SpriteBatch sb ) {
			return true;
		}

		protected virtual void PostDrawSelf( bool isSelfDrawn, SpriteBatch sb ) {
		}


		////////////////

		private void DrawOverlays_If( SpriteBatch sb ) {
			bool isHoverCollision = false;
			bool isHoverAnchorRight = false;
			bool isHoverAnchorBottom = false;
			bool isHoverReset = false;

			//if( Main.playerInventory ) {
			//bool mode = Main.keyState.IsKeyDown( Keys.LeftAlt )
			//	|| Main.keyState.IsKeyDown( Keys.RightAlt );
			bool editMode = HUDElementsLibAPI.IsEditModeActive();

			if( editMode ) {
				this.DrawEditModeTitle( sb );
				this.DrawEditModeBoxes( sb );
				this.DrawEditModeControls_If(
					sb,
					out isHoverCollision,
					out isHoverReset,
					out isHoverAnchorRight,
					out isHoverAnchorBottom
				);
			}

			//

			(string text, int duration) hoverInfo = this.GetHoverText(
				editMode,
				isHoverCollision,
				isHoverReset,
				isHoverAnchorRight,
				isHoverAnchorBottom
			);

			if( !string.IsNullOrEmpty(hoverInfo.text ) ) {
				this.DrawHoverText_If( sb, hoverInfo.text, hoverInfo.duration );
			} else {
				this.ClearHoverText();
			}
		}
	}
}