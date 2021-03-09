using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public static Rectangle GetCollisionTogglerForBox( Rectangle area ) {
			return new Rectangle( area.Left, area.Top, 16, 16 );
		}
		
		public static Rectangle GetAnchorButtonIconForBox( Rectangle area ) {
			return new Rectangle( area.Right - 24, area.Bottom - 24, 24, 24 );
		}

		public static Rectangle GetRightAnchorButtonForBox( Rectangle area ) {
			return new Rectangle( area.Right - 16, area.Top, 16, area.Height - 16 );
		}

		public static Rectangle GetBottomAnchorButtonForBox( Rectangle area ) {
			return new Rectangle( area.Left, area.Bottom - 16, area.Width - 16, 16 );
		}



		////////////////

		public virtual Vector2 GetCustomPositionOnHUD( bool withoutDisplacement ) {
			Vector2 pos;

			if( !withoutDisplacement && this.DisplacedPosition.HasValue ) {
				pos.X = (int)this.DisplacedPosition.Value.X;
				pos.Y = (int)this.DisplacedPosition.Value.Y;
			} else {
				pos = new Vector2(
					this.CustomPositionWithAnchors.X < 0
						? Main.screenWidth + this.CustomPositionWithAnchors.X
						: this.CustomPositionWithAnchors.X,
					this.CustomPositionWithAnchors.Y < 0
						? Main.screenHeight + this.CustomPositionWithAnchors.Y
						: this.CustomPositionWithAnchors.Y
				);
			}

			return pos;
		}

		////

		public virtual Vector2 GetCustomDimensionsOnHUD() {
			return new Vector2( this.CustomDimensions.X, this.CustomDimensions.Y );
		}


		////////////////

		public Rectangle GetAreaOnHUD( bool withoutDisplacement ) {
			Vector2 pos = this.GetCustomPositionOnHUD( withoutDisplacement );
			Vector2 dim = this.GetCustomDimensionsOnHUD();

			return new Rectangle(
				(int)pos.X,
				(int)pos.Y,
				(int)dim.X,
				(int)dim.Y
			);
		}


		////////////////

		public void SetCustomPosition( Vector2 pos, bool preserveExistingAnchors ) {
			if( preserveExistingAnchors ) {
				if( this.CustomPositionWithAnchors.X < 0 && pos.X >= 0 ) {
					pos.X -= Main.screenWidth;
				}
				if( this.CustomPositionWithAnchors.Y < 0 && pos.Y >= 0 ) {
					pos.Y -= Main.screenHeight;
				}
			}
			this.CustomPositionWithAnchors = pos;
		}


		////////////////
		
		public void ToggleRightAnchor() {
			Vector2 pos = this.GetCustomPositionOnHUD( true );
			pos.Y = this.CustomPositionWithAnchors.Y;

			if( this.CustomPositionWithAnchors.X >= 0 ) {
				pos.X -= Main.screenWidth;
			}

			this.SetCustomPosition( pos, false );
		}

		public void ToggleBottomAnchor() {
			Vector2 pos = this.GetCustomPositionOnHUD( true );
			pos.X = this.CustomPositionWithAnchors.X;

			if( this.CustomPositionWithAnchors.Y >= 0 ) {
				pos.Y -= Main.screenHeight;
			}

			this.SetCustomPosition( pos, false );
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