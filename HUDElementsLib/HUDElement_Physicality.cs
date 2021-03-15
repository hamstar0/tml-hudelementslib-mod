using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public bool IsRightAnchored() => this.CustomPositionWithAnchor.X < 0f;

		public bool IsBottomAnchored() => this.CustomPositionWithAnchor.Y < 0f;


		////

		public virtual bool AutoAnchors() {
			return true;
		}


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

		public virtual Vector2 GetHUDComputedPosition( bool applyDisplacement ) {
			if( applyDisplacement && this.DisplacedPosition.HasValue ) {
				return this.DisplacedPosition.Value;
			} else {
				return this.GetAnchorComputedPosition();
			}
		}

		////

		public virtual Vector2 GetHUDComputedDimensions() {
			return this.CustomDimensions;
		}


		////////////////

		public Rectangle GetHUDComputedArea( bool applyDisplacement ) {
			Vector2 pos = this.GetHUDComputedPosition( applyDisplacement );
			Vector2 dim = this.GetHUDComputedDimensions();

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

			if( this.AutoAnchors() ) {
				Vector2 hudPos = this.GetAnchorComputedPosition();

				if( hudPos.X >= (Main.screenWidth/2) ) {
					this.CustomPositionWithAnchor.X = hudPos.X - Main.screenWidth;
				}
				if( hudPos.Y >= (Main.screenHeight/2) ) {
					this.CustomPositionWithAnchor.Y = hudPos.Y - Main.screenHeight;
				}
			}
		}


		////////////////
		
		public void ToggleRightAnchor() {
			Vector2 pos = this.GetHUDComputedPosition( false );  // flips anchors if negative
			pos.Y = this.CustomPositionWithAnchor.Y;

			if( this.CustomPositionWithAnchor.X >= 0 ) {	// flips X anchor if positive
				pos.X -= Main.screenWidth;
			}

			this.SetUncomputedPosition( pos, false );
		}

		public void ToggleBottomAnchor() {
			Vector2 pos = this.GetHUDComputedPosition( false );  // flips anchors if negative
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