using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public static Rectangle GetCollisionTogglerForBox( Rectangle area ) {
			return new Rectangle( area.Left, area.Top, 24, 24 );
		}

		public static Rectangle GetCollisionTogglerIconForBox( Rectangle area ) {
			return new Rectangle( area.Left+2, area.Top+2, 20, 20 );
		}
		
		public static Rectangle GetAnchorButtonIconForBox( Rectangle area ) {
			return new Rectangle( area.Right - 18, area.Bottom - 18, 16, 16 );
		}
		
		public static Rectangle GetAnchorButtonIconBgForBox( Rectangle area ) {
			return new Rectangle( area.Right - 20, area.Bottom - 20, 20, 20 );
		}

		public static Rectangle GetRightAnchorButtonForBox( Rectangle area ) {
			return new Rectangle( area.Right - 16, area.Top, 16, area.Height - 20 );
		}

		public static Rectangle GetBottomAnchorButtonForBox( Rectangle area ) {
			return new Rectangle( area.Left, area.Bottom - 16, area.Width - 20, 16 );
		}



		////////////////

		public bool IsRightAnchored() => this.CustomPositionWithAnchor.X < 0f;

		public bool IsBottomAnchored() => this.CustomPositionWithAnchor.Y < 0f;


		////////////////

		public virtual Vector2 GetPositionAndAnchors() {
			return this.CustomPositionWithAnchor;
		}

		public virtual Vector2 GetAnchorComputedPosition() {
			return new Vector2(
				this.CustomPositionWithAnchor.X < 0
					? Main.screenWidth + this.CustomPositionWithAnchor.X
					: this.CustomPositionWithAnchor.X,
				this.CustomPositionWithAnchor.Y < 0
					? Main.screenHeight + this.CustomPositionWithAnchor.Y
					: this.CustomPositionWithAnchor.Y
			);
		}

		public virtual Vector2 GetHudComputedPosition( bool withoutDisplacement ) {
			if( !withoutDisplacement && this.DisplacedPosition.HasValue ) {
				return this.DisplacedPosition.Value;
			} else {
				return this.GetAnchorComputedPosition();
			}
		}

		////

		public virtual Vector2 GetHudComputedDimensions() {
			return this.CustomDimensions;
		}


		////////////////

		public Rectangle GetHudComputedArea( bool withoutDisplacement ) {
			Vector2 pos = this.GetHudComputedPosition( withoutDisplacement );
			Vector2 dim = this.GetHudComputedDimensions();

			return new Rectangle(
				(int)pos.X,
				(int)pos.Y,
				(int)dim.X,
				(int)dim.Y
			);
		}


		////////////////

		public void SetUncomputedPosition( Vector2 pos, bool conveyAnyExistingAnchors ) {
			if( conveyAnyExistingAnchors ) {
				if( this.CustomPositionWithAnchor.X < 0 && pos.X >= 0 ) {
					pos.X -= Main.screenWidth;
				}
				if( this.CustomPositionWithAnchor.Y < 0 && pos.Y >= 0 ) {
					pos.Y -= Main.screenHeight;
				}
			}
			this.CustomPositionWithAnchor = pos;
		}


		////////////////
		
		public void ToggleRightAnchor() {
			Vector2 pos = this.GetHudComputedPosition( true );  // flips anchors if negative
			pos.Y = this.CustomPositionWithAnchor.Y;

			if( this.CustomPositionWithAnchor.X >= 0 ) {	// flips X anchor if positive
				pos.X -= Main.screenWidth;
			}

			this.SetUncomputedPosition( pos, false );
		}

		public void ToggleBottomAnchor() {
			Vector2 pos = this.GetHudComputedPosition( true );  // flips anchors if negative
			pos.X = this.CustomPositionWithAnchor.X;

			if( this.CustomPositionWithAnchor.Y >= 0 ) {    // flips Y anchor if positive
				pos.Y -= Main.screenHeight;
			}

			this.SetUncomputedPosition( pos, false );
		}


		////////////////

		internal void SetDisplacedPosition( Vector2 pos ) {
			this.DisplacedPosition = pos;
		}

		internal void RevertDisplacedPosition() {
			this.DisplacedPosition = null;
		}
	}
}