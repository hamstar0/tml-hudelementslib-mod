using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	public partial class HUDElement : UIElement {
		public static Rectangle GetCollisionTogglerArea( Rectangle area ) {
			return new Rectangle( area.Left, area.Top, 20, 20 );
		}

		public static Rectangle GetCollisionTogglerIconArea( Rectangle area ) {
			return new Rectangle( area.Left+1, area.Top+1, 18, 18 );
		}
		
		
		public static Rectangle GetResetButtonArea( Rectangle area ) {
			return new Rectangle( area.Left + 20, area.Top, 20, 20 );
		}

		public static Rectangle GetResetButtonIconArea( Rectangle area ) {
			return new Rectangle( area.Left + 21, area.Top+1, 18, 18 );
		}
		

		public static Rectangle GetAnchorButtonIconArea( Rectangle area ) {
			return new Rectangle( area.Right - 15, area.Bottom - 15, 14, 14 );
		}
		
		public static Rectangle GetAnchorButtonIconBgArea( Rectangle area ) {
			return new Rectangle( area.Right - 16, area.Bottom - 16, 16, 16 );
		}


		public static Rectangle GetRightAnchorButtonArea( Rectangle area ) {
			return new Rectangle( area.Right - 8, area.Top, 8, area.Height - 16 );
		}

		public static Rectangle GetBottomAnchorButtonArea( Rectangle area ) {
			return new Rectangle( area.Left, area.Bottom - 8, area.Width - 16, 8 );
		}
	}
}