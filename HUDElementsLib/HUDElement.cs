using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private Vector2? DesiredDragPosition = null;
		private Vector2 PreviousDragMousePos = default;

		private Vector2? PreDisplacementPos = null;


		////////////////

		private Vector2 Position;
		private Vector2 Dimensions;


		////////////////

		public string Name { get; private set; }

		public bool IsHovering { get; private set; } = false;


		////////////////

		public bool IsDragging => this.DesiredDragPosition.HasValue;



		////////////////

		public HUDElement( string name, Vector2 position, Vector2 dimensions ) : base() {
			this.Name = name;
			this.Position = position;
			this.Dimensions = dimensions;

			this.UpdateHUDPosition();
		}


		////////////////

		public virtual Vector2 GetDisplacementDirection() {
			Vector2 scrPos = this.GetPositionOnHUD( false );
			Vector2 baseScrDim = this.GetDimensionsOnHUD();
			float x = scrPos.X + baseScrDim.X;
			float y = scrPos.Y + baseScrDim.Y;
			float midX = Main.screenWidth / 2;
			float midY = Main.screenHeight / 2;

			x -= midX;
			y -= midY;

			return Vector2.Normalize( new Vector2(x, y) );
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