using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		private Vector2? DesiredDragPosition = null;
		private Vector2 PreviousDragMousePos = default;


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

		public Rectangle GetRect() {
			return new Rectangle(
				(int)this.Left.Pixels,
				(int)this.Top.Pixels,
				(int)this.Width.Pixels,
				(int)this.Height.Pixels
			);
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

		public void SetPosition( Vector2 pos ) {
			this.Left.Pixels = pos.X;
			this.Top.Pixels = pos.Y;
		}


		////////////////

		public virtual bool ConsumesCursor() {
			return this.IsDragging;
		}


		////////////////
		
		public override void Update( GameTime gameTime ) {
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