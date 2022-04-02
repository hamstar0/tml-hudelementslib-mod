using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	partial class HUDManager {
		public struct ElementInfo {
			public Vector2 ScreenPosition;
			public bool IsIgnoringCollisions;
		}



		////////////////

		//internal IDictionary<string, ISet<HUDElement>> Elements = new Dictionary<string, ISet<HUDElement>>();
		public IDictionary<string, HUDElement> Elements = new Dictionary<string, HUDElement>();
		public IDictionary<string, ElementInfo> SavedElementInfo = new Dictionary<string, ElementInfo>();

		private UIState MyUI;



		////////////////

		public HUDManager( UIState myUI ) {
			this.MyUI = myUI;
		}


		////////////////
		
		public void LoadHUDElementInfo( string name, float x, float y, bool isIgnoringCollisions ) {
			var pos = new Vector2( x, y );

			if( this.Elements.ContainsKey(name) ) {
				this.Elements[name].SetUncomputedPosition( pos, false );
				this.Elements[name].IsIgnoringCollisions = isIgnoringCollisions;
				this.Elements[name].Recalculate();
			} else {
				this.SavedElementInfo[name] = new ElementInfo {
					ScreenPosition = pos,
					IsIgnoringCollisions = isIgnoringCollisions
				};
			}
		}

		public void LoadHUDElement( HUDElement element ) {
			this.Elements[ element.Name ] = element;

			if( this.SavedElementInfo.ContainsKey(element.Name) ) {
				ElementInfo elemInfo = this.SavedElementInfo[ element.Name ];

				this.SavedElementInfo.Remove( element.Name );

				element.SetUncomputedPosition( elemInfo.ScreenPosition, false );
				element.IsIgnoringCollisions = elemInfo.IsIgnoringCollisions;
				element.Recalculate();
			}
		}


		////////////////
		
		internal void PreUpdateForInteractions() {
			foreach( HUDElement elem in this.Elements.Values ) {
				if( elem.UpdateEditModeInteractionsWithinEntireUI() ) {
					break;
				}
			}

			if( !HUDElementsLibAPI.IsEditModeActive() ) {
				bool hasInteracted = false;

				foreach( HUDElement elem in this.Elements.Values ) {
					hasInteracted = this.UpdateInteractionsWithinEntireUI_If( elem );

					if( hasInteracted ) {
						break;
					}
				}

				if( !hasInteracted ) {
					this.ClearInteractionsIfAny();
				}
			}
		}
	}
}