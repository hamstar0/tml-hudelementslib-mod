using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private Vector2? DesiredDragPosition = null;
		private Vector2 PreviousDragMousePos = default;

		/// <summary>
		/// Position offset of element after displacement from collisions with other elements.
		/// </summary>
		protected Vector2? DisplacedOffset = null;

		private bool IsMouseHoveringEditableBox = false;


		////////////////

		/// <summary>
		/// Original starting screen offset amount from percent anchors. Element will return here when user sets it to reset.
		/// </summary>
		protected Vector2 OriginalPositionOffset;
		/// <summary></summary>
		protected Vector2 OriginalPositionPercent;

		/// <summary>Get screen offset amount from percent anchors.</summary>
		protected Vector2 CurrentPositionOffset;
		/// <summary></summary>
		protected Vector2 CurrentPositionPercent;

		/// <summary></summary>
		protected Vector2 CurrentDimensions;


		/// <summary></summary>
		public string Name { get; private set; }


		/// <summary></summary>
		public virtual bool IsIgnoringCollisions { get; protected internal set; } = false;


		/// <summary>Reports is the widget is enabled (visible and interactable).</summary>
		public Func<bool> Enabler { get; private set; }


		/// <summary></summary>
		public bool IsInteractingWithControls { get; private set; } = false;

		/// <summary></summary>
		public bool IsDraggingSinceLastTick => this.DesiredDragPosition.HasValue;

		/// <summary></summary>
		public bool IsInteractingAny => this.IsInteractingWithControls || this.IsDraggingSinceLastTick;



		////////////////

		public HUDElement(
					string name,
					Vector2 positionOffset,
					Vector2 positionPercent,
					Vector2 dimensions,
					Func<bool> enabler ) : base() {
			this.Name = name;

			this.OriginalPositionOffset = positionOffset;
			this.CurrentPositionOffset = positionOffset;

			this.OriginalPositionPercent = positionPercent;
			this.CurrentPositionPercent = positionPercent;

			this.CurrentDimensions = dimensions;

			this.Enabler = enabler;

			//

			this.Left.Set( positionOffset.X, positionPercent.X );
			this.Top.Set( positionOffset.Y, positionPercent.Y );

			this.Width.Set( this.CurrentDimensions.X, 0f );
			this.Height.Set( this.CurrentDimensions.Y, 0f );
		}


		////////////////

		/// <summary>Gets the unit vector of the preferred direction to offset the element to find a collision-free
		/// position.</summary>
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
			this.CurrentPositionOffset = this.OriginalPositionOffset;
		}
	}
}