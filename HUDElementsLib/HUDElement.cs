using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private Vector2? DesiredDragPosition = null;
		private Vector2 PreviousDragMousePos = default;

		private (int x, int y)? PreDisplacementPos = null;


		////////////////
		
		public string Name { get; private set; }

		public bool IsHovering { get; private set; } = false;


		////////////////

		public bool IsDragging => this.DesiredDragPosition.HasValue;



		////////////////

		public HUDElement( string name ) : base() {
			this.Name = name;
		}


		////////////////

		public Rectangle GetRect( bool withoutDisplacement = false ) {
			var rect = new Rectangle(
				(int)this.Left.Pixels,
				(int)this.Top.Pixels,
				(int)this.Width.Pixels,
				(int)this.Height.Pixels
			);

			if( !withoutDisplacement && this.PreDisplacementPos.HasValue ) {
				rect.X = this.PreDisplacementPos.Value.x;
				rect.Y = this.PreDisplacementPos.Value.y;
			}

			return rect;
		}

		public virtual Vector2 GetDisplacementDirection() {
			int x = (int)(this.Left.Pixels + this.Width.Pixels);
			int y = (int)(this.Left.Pixels + this.Width.Pixels);
			int midX = Main.screenWidth / 2;
			int midY = Main.screenHeight / 2;

			x -= midX;
			y -= midY;

			return Vector2.Normalize( new Vector2(x, y) );
		}

		////

		public void SetDisplacedPosition( Vector2 pos ) {
			this.PreDisplacementPos = ( (int)this.Left.Pixels, (int)this.Top.Pixels );
			this.Left.Pixels = pos.X;
			this.Top.Pixels = pos.Y;
		}

		public void RevertDisplacedPosition() {
			if( !this.PreDisplacementPos.HasValue ) {
				return;
			}
			this.Left.Pixels = this.PreDisplacementPos.Value.x;
			this.Top.Pixels = this.PreDisplacementPos.Value.y;

			this.PreDisplacementPos = null;
		}

		////////////////

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
				this.RunHUDEditorIf( out bool isHovering );
				this.IsHovering = isHovering;
			} else {
				this.IsHovering = false;

				this.DesiredDragPosition = null;
			}
		}
	}
}