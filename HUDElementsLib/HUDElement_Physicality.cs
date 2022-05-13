using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public static Vector2 ComputeScreenPosition( Vector2 positionOffset, Vector2 positionPercent ) {
			return new Vector2(
				(positionPercent.X * (float)Main.screenWidth ) + (float)positionOffset.X,
				(positionPercent.Y * (float)Main.screenHeight ) + (float)positionOffset.Y
			);
		}

		public static void FitOffsetToScreen( ref Vector2 positionOffset, Vector2 positionPercent, Vector2 dim ) {
			Vector2 origScrPos = HUDElement.ComputeScreenPosition( positionOffset, positionPercent );
			Vector2 origEdges = origScrPos + dim;

			if( origScrPos.X < 0f ) {
				positionOffset.X += -origScrPos.X;
			} else if( origEdges.X > Main.screenWidth ) {
				positionOffset.X += origEdges.X - Main.screenWidth;
			}

			if( origScrPos.Y < 0f ) {
				positionOffset.Y += -origScrPos.Y;
			} else if( origEdges.Y > Main.screenHeight ) {
				positionOffset.Y += origEdges.Y - Main.screenHeight;
			}
		}



		////////////////

		public virtual bool AutoAnchors() {
			return true;
		}


		////////////////

		/// <summary>
		/// Gets position relative to anchors, and also the anchor percents.
		/// </summary>
		/// <returns></returns>
		public virtual (Vector2 relative, Vector2 percent) GetIntendedPosition() {
			return (this.CurrentPositionOffset, this.CurrentPositionPercent);
		}

		/// <summary>
		/// Gets intended screen position of element.
		/// </summary>
		/// <returns></returns>
		public virtual Vector2 GetComputedIntendedPosition() {
			return HUDElement.ComputeScreenPosition( this.CurrentPositionOffset, this.CurrentPositionPercent );
		}

		/// <summary>
		/// Gets the final position the element will appear and be interactable at.
		/// </summary>
		/// <param name="applyDisplacement">Gets the position of the element after being displaced by collisions.</param>
		/// <returns></returns>
		public virtual Vector2 GetHUDComputedPosition( bool applyDisplacement ) {
			Vector2 scrPos = this.GetComputedIntendedPosition();

			if( applyDisplacement && this.DisplacedOffset.HasValue ) {
				return scrPos + this.DisplacedOffset.Value;
			} else {
				return scrPos;
			}
		}

		////

		/// <summary>
		/// Gets the final dimensions the element will appear with.
		/// </summary>
		/// <returns></returns>
		public virtual Vector2 GetHUDComputedDimensions() {
			return this.CurrentDimensions;
		}


		////////////////

		/// <summary>
		/// Gets the final dimensions and position the element will appear and be interactable with.
		/// </summary>
		/// <param name="applyDisplacement"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Sets the desired position of the element, not factoring collisions.
		/// </summary>
		/// <param name="relPos"></param>
		/// <param name="conveyPercent">Translates the given screen position to be relative to the internal percent
		/// position.</param>
		public void SetIntendedPosition( Vector2 screenPos, bool conveyPercent ) {
			Vector2 relPos = screenPos;
			Vector2 perc = default;

			if( conveyPercent ) {
				perc = this.CurrentPositionPercent;

				relPos.X -= (float)Main.screenWidth * perc.X;
				relPos.Y -= (float)Main.screenHeight * perc.Y;
			}

			this.SetIntendedPosition( relPos, perc );
		}

		/// <summary>
		/// Sets the desired position of the element, not factoring collisions.
		/// </summary>
		/// <param name="relPos"></param>
		/// <param name="percPos"></param>
		public void SetIntendedPosition( Vector2 relPos, Vector2 percPos ) {
			this.CurrentPositionOffset = relPos;
			this.CurrentPositionPercent = percPos;
		}


		////////////////

		internal void SetDisplacedOffset( Vector2 offset ) {
			this.DisplacedOffset = offset;
		}

		internal void RevertDisplacedOffset() {
			this.DisplacedOffset = null;
		}
	}
}