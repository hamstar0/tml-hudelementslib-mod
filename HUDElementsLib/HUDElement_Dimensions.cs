using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public void SetBasePosition( Vector2 pos ) {
			this.Position = pos;
		}


		////////////////

		internal void SetDisplacedPosition( Vector2 pos ) {
			this.DisplacedPosition = pos;
		}

		internal void RevertDisplacedPosition() {
			this.DisplacedPosition = null;
		}


		////////////////

		public Vector2 GetPositionOnHUD( bool withoutDisplacement ) {
			Vector2 pos;

			if( !withoutDisplacement && this.DisplacedPosition.HasValue ) {
				pos.X = (int)this.DisplacedPosition.Value.X;
				pos.Y = (int)this.DisplacedPosition.Value.Y;
			} else {
				pos = new Vector2(
					this.Position.X < 0
						? Main.screenWidth + this.Position.X
						: this.Position.X,
					this.Position.Y < 0
						? Main.screenHeight + this.Position.Y
						: this.Position.Y
				);
			}

			return pos;
		}

		////

		public Vector2 GetDimensionsOnHUD() {
			return new Vector2( this.Dimensions.X, this.Dimensions.Y );
		}

		////

		public Rectangle GetAreaOnHUD( bool withoutDisplacement ) {
			Vector2 scrPos = this.GetPositionOnHUD( withoutDisplacement );
			Vector2 baseScrDim = this.GetDimensionsOnHUD();

			return new Rectangle(
				(int)scrPos.X,
				(int)scrPos.Y,
				(int)baseScrDim.X,
				(int)baseScrDim.Y
			);
		}
	}
}