using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private Vector2? DesiredDragPosition = null;
		private Vector2 PreviousDragMousePos = default;

		protected Vector2? DisplacedPosition = null;

		private bool IsMouseHoveringEditableBox = false;


		////////////////

		protected Vector2 DefaultPositionWithAnchor;
		protected Vector2 CustomPositionWithAnchor;
		protected Vector2 CustomDimensions;


		////////////////

		public string Name { get; private set; }

		////////////////

		public virtual bool IsIgnoringCollisions { get; protected internal set; } = false;


		////////////////

		public Func<bool> Enabler { get; private set; }


		////////////////

		public bool IsInteractingWithControls { get; private set; } = false;

		public bool IsDraggingSinceLastTick => this.DesiredDragPosition.HasValue;

		public bool IsInteractingAny => this.IsInteractingWithControls || this.IsDraggingSinceLastTick;



		////////////////

		public HUDElement( string name, Vector2 position, Vector2 dimensions, Func<bool> enabler ) : base() {
			this.Name = name;
			this.DefaultPositionWithAnchor = position;
			this.CustomPositionWithAnchor = position;
			this.CustomDimensions = dimensions;
			this.Enabler = enabler;

			this.Left.Set( position.X, 0f );
			this.Top.Set( position.Y, 0f );
			this.Width.Set( this.CustomDimensions.X, 0f );
			this.Height.Set( this.CustomDimensions.Y, 0f );
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

			return Vector2.Normalize( posMid ); // by default, aim to screen center
		}


		////////////////

		public virtual bool SkipSave() {
			return false;
		}
		
		////

		public virtual bool IsEnabled() {
			return this.Enabler.Invoke();
		}

		////

		public virtual bool ConsumesCursor() {
			return this.IsDraggingSinceLastTick;
		}


		////////////////

		public void ResetPositionToDefault() {
			this.CustomPositionWithAnchor = this.DefaultPositionWithAnchor;
		}
	}
}