using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public virtual Vector2 GetPositionOnHUD( bool withoutDisplacement ) {
			Vector2 pos;

			if( !withoutDisplacement && this.DisplacedPosition.HasValue ) {
				pos.X = (int)this.DisplacedPosition.Value.X;
				pos.Y = (int)this.DisplacedPosition.Value.Y;
			} else {
				pos = new Vector2(
					this.CustomPosition.X < 0
						? Main.screenWidth + this.CustomPosition.X
						: this.CustomPosition.X,
					this.CustomPosition.Y < 0
						? Main.screenHeight + this.CustomPosition.Y
						: this.CustomPosition.Y
				);
			}

			return pos;
		}

		////

		public virtual Vector2 GetDimensionsOnHUD() {
			return new Vector2( this.CustomDimensions.X, this.CustomDimensions.Y );
		}


		////////////////

		public Rectangle GetAreaOnHUD( bool withoutDisplacement ) {
			Vector2 pos = this.GetPositionOnHUD( withoutDisplacement );
			Vector2 dim = this.GetDimensionsOnHUD();

			return new Rectangle(
				(int)pos.X,
				(int)pos.Y,
				(int)dim.X,
				(int)dim.Y
			);
		}


		////////////////

		public void SetBasePosition( Vector2 pos ) {
			this.CustomPosition = pos;
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