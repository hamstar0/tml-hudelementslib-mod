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


		////////////////

		public override void Draw( SpriteBatch spriteBatch ) {
			base.Draw( spriteBatch );

			if( this.IsHovering && !this.IsDragging ) {
				Utils.DrawBorderStringFourWay(
					sb: spriteBatch,
					font: Main.fontMouseText,
					text: "Alt+Click to drag",
					x: Main.MouseScreen.X + 12,
					y: Main.MouseScreen.Y + 16,
					textColor: new Color( Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor ),
					borderColor: Color.Black,
					origin: default
				);
			}
		}
	}
}