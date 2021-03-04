using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	partial class HUDManager {
		//internal IDictionary<string, ISet<HUDElement>> Elements = new Dictionary<string, ISet<HUDElement>>();
		public IDictionary<string, HUDElement> Elements = new Dictionary<string, HUDElement>();
		public IDictionary<string, (float x, float y)> SavedElementInfo = new Dictionary<string, (float, float)>();

		private UIState MyUI;



		////////////////

		public HUDManager( UIState myUI ) {
			this.MyUI = myUI;
		}


		////////////////
		
		public void LoadHUDElementAsInfo( string name, float x, float y ) {
			if( this.Elements.ContainsKey(name) ) {
				this.Elements[name].Left.Pixels = x;
				this.Elements[name].Top.Pixels = y;
			} else {
				this.SavedElementInfo[name] = (x, y);
			}
		}

		public void LoadHUDElementFromInfo( HUDElement element ) {
			if( !this.SavedElementInfo.ContainsKey(element.Name) ) {
				return;
			}

			(float x, float y) elemInfo = this.SavedElementInfo[ element.Name ];
			this.SavedElementInfo.Remove( element.Name );

			element.Left.Pixels = elemInfo.x;
			element.Top.Pixels = elemInfo.y;

			this.Elements[ element.Name ] = element;
		}


		////////////////

		public Vector2 HandleFirstFoundCollision( HUDElement element ) {
			HUDCollision collDec = element.CollisionDecision();
			if( collDec == HUDCollision.None ) {
				return element.DesiredPosition;
			}

			foreach( HUDElement elem in this.Elements.Values ) {
				if( elem == element ) {
					continue;
				}
				if( !element.CollidesWith( elem ) ) {
					continue;
				}

				switch( element.CollisionDecision() ) {
				case HUDCollision.Smart:
					return element.ResolveCollisionSmart( elem );
				case HUDCollision.Custom:
					return element.ResolveCollisionCustom( elem );
				}

				break;
			}

			return element.DesiredPosition;
		}
	}
}