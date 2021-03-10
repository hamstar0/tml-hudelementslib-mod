using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private Vector2? DesiredDragPosition = null;
		private Vector2 PreviousDragMousePos = default;

		protected Vector2? DisplacedPosition = null;


		////////////////

		protected Vector2 CustomPositionWithAnchor;
		protected Vector2 CustomDimensions;


		////////////////

		public string Name { get; private set; }

		public bool IsPressingControl { get; private set; } = false;

		public bool IsHovering { get; private set; } = false;

		////

		public bool IsDragging => this.DesiredDragPosition.HasValue;


		////////////////

		public virtual bool IsIgnoringCollisions { get; protected set; } = false;

		public virtual bool CanToggleCollisions { get; protected set; } = true;



		////////////////

		public HUDElement( string name, Vector2 position, Vector2 dimensions ) : base() {
			this.Name = name;
			this.CustomPositionWithAnchor = position;
			this.CustomDimensions = dimensions;
		}


		////////////////

		public virtual Vector2 GetDisplacementDirection( HUDElement against ) {
			Vector2 pos = this.GetHudComputedPosition( true );
			Vector2 dim = this.GetHudComputedDimensions();
			Vector2 posMid = pos + (dim * 0.5f);
			float midX = Main.screenWidth / 2;
			float midY = Main.screenHeight / 2;

			posMid.X = midX - posMid.X;
			posMid.Y = midY - posMid.Y;

			return Vector2.Normalize( posMid );	// by default, aim to screen center
		}


		////////////////

		public virtual bool SkipSave() {
			return false;
		}
		
		public virtual bool IsEnabled() {
			return true;
		}

		public virtual bool IsLocked() {
			return false;
		}

		////

		public virtual bool ConsumesCursor() {
			return this.IsDragging;
		}
	}
}