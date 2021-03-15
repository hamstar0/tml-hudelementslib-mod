using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private Vector2? DesiredDragPosition = null;
		private Vector2 PreviousDragMousePos = default;

		protected Vector2? DisplacedPosition = null;

		private bool IsMouseHovering_Custom = false;


		////////////////

		protected Vector2 CustomPositionWithAnchor;
		protected Vector2 CustomDimensions;


		////////////////

		public string Name { get; private set; }

		////////////////

		public virtual bool IsIgnoringCollisions { get; protected set; } = false;


		////////////////

		public bool IsInteractingWithControls { get; private set; } = false;

		public bool IsDraggingSinceLastTick => this.DesiredDragPosition.HasValue;

		public bool IsInteractingAny => this.IsInteractingWithControls || this.IsDraggingSinceLastTick;



		////////////////

		public HUDElement( string name, Vector2 position, Vector2 dimensions ) : base() {
			this.Name = name;
			this.CustomPositionWithAnchor = position;
			this.CustomDimensions = dimensions;
		}


		////////////////

		public virtual Vector2 GetDisplacementDirection( HUDElement against ) {
			Vector2 pos = this.GetHUDComputedPosition( false );
			Vector2 dim = this.GetHUDComputedDimensions();
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
		
		////

		public virtual bool IsEnabled() {
			return true;
		}

		////

		public virtual bool ConsumesCursor() {
			return this.IsDraggingSinceLastTick;
		}


		////////////////

		public virtual (string text, int duration) GetHoverText(
					bool editMode,
					bool isCollisionToggleButton,
					bool isAnchorRightToggle,
					bool isAnchorBottomToggle ) {
			if( !this.IsMouseHovering_Custom ) {
				return ("", -1);
			}

			if( editMode ) {
				if( this.IsCollisionToggleable() ) {
					if( isCollisionToggleButton ) {
						return ("Toggle collisions", -1);
					}
				}

				if( this.IsAnchorsToggleable() ) {
					if( isAnchorRightToggle ) {
						return ("Anchor to right edge of screen", -1);
					}
					if( isAnchorBottomToggle ) {
						return ("Anchor to bottom edge of screen", -1);
					}
				}

				if( !this.IsDragLocked() ) {
					return ("Alt+Click to drag", -1);
				}
			} else {
				if( Main.playerInventory ) {
					return ("Bind 'Edit Mode' to a key to interact.", 45);
				}
			}

			return ("", -1);
		}
	}
}