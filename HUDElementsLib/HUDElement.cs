using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
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


		////////////////

		public virtual bool ConsumesCursor() {
			return this.IsDragging;
		}


		////////////////
		
		public override void Update( GameTime gameTime ) {
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