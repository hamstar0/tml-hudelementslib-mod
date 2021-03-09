using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private Vector2? DesiredDragPosition = null;
		private Vector2 PreviousDragMousePos = default;

		protected Vector2? DisplacedPosition = null;


		////////////////

		protected Vector2 CustomPosition;
		protected Vector2 CustomDimensions;


		////////////////

		public string Name { get; private set; }

		public bool IsPressingControl { get; private set; } = false;

		public bool IsHovering { get; private set; } = false;

		////////////////

		public bool IsDragging => this.DesiredDragPosition.HasValue;



		////////////////

		public HUDElement( string name, Vector2 position, Vector2 dimensions ) : base() {
			this.Name = name;
			this.CustomPosition = position;
			this.CustomDimensions = dimensions;
		}


		////////////////

		public virtual Vector2 GetDisplacementDirection() {
			Vector2 pos = this.GetPositionOnHUD( true );
			Vector2 dim = this.GetDimensionsOnHUD();
			Vector2 posMid = pos + (dim * 0.5f);
			float midX = Main.screenWidth / 2;
			float midY = Main.screenHeight / 2;

			posMid.X = midX - posMid.X;
			posMid.Y = midY - posMid.Y;

			return Vector2.Normalize( posMid );
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

		public virtual bool CanToggleCollisions() {
			return this.IsEnabled() && !this.IsLocked();
		}

		////

		public virtual bool ConsumesCursor() {
			return this.IsDragging;
		}


		////////////////

		public bool IsRightAnchored() => this.CustomPosition.X < 0f;

		public bool IsBottomAnchored() => this.CustomPosition.Y < 0f;


		////////////////

		public override void Update( GameTime gameTime ) {
			if( !this.IsEnabled() ) {
				return;
			}

			ModContent.GetInstance<HUDElementsLibMod>()
				.HUDManager
				.ApplyDisplacementsIf( this );

			if( Main.playerInventory ) {
				this.UpdateInteractionsIf( out bool isHovering );
				this.IsHovering = isHovering;
			} else {
				this.IsHovering = false;

				this.DesiredDragPosition = null;
			}

			this.UpdateHUDPosition();
		}

		////

		private void UpdateHUDPosition() {
			Vector2 pos = this.GetPositionOnHUD( false );
			Vector2 dim = this.GetDimensionsOnHUD();

			this.Left.Pixels = pos.X;
			this.Top.Pixels = pos.Y;
			this.Width.Pixels = dim.X;
			this.Height.Pixels = dim.Y;
		}
	}
}