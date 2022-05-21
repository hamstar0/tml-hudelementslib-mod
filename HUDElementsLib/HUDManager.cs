using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	partial class HUDManager {
		public struct ElementInfo {
			public Vector2 RelativePosition;
			public Vector2 PositionPercent;
			public bool? IsIgnoringCollisions;
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
		
		public void LoadHUDElementInfo( string name, Vector2 relPos, Vector2 percPos, bool? isIgnoringCollisions ) {
			if( this.Elements.ContainsKey(name) ) {
				this.Elements[name].SetIntendedPosition( relPos, percPos );
				if( isIgnoringCollisions.HasValue ) {
					this.Elements[name].IsIgnoringCollisions = isIgnoringCollisions.Value;
				}

				this.Elements[name].Recalculate();
			} else {
				this.SavedElementInfo[name] = new ElementInfo {
					RelativePosition = relPos,
					PositionPercent = percPos,
					IsIgnoringCollisions = isIgnoringCollisions
				};
			}
		}

		public void LoadHUDElement( HUDElement element ) {
			this.Elements[ element.Name ] = element;

			//

			if( this.SavedElementInfo.ContainsKey(element.Name) ) {
				ElementInfo elemInfo = this.SavedElementInfo[ element.Name ];

				this.SavedElementInfo.Remove( element.Name );

				//

				element.SetIntendedPosition( elemInfo.RelativePosition, elemInfo.PositionPercent );

				if( elemInfo.IsIgnoringCollisions.HasValue ) {
					element.IsIgnoringCollisions = elemInfo.IsIgnoringCollisions.Value;
				}
				element.Recalculate();
			}
		}


		////////////////
		
		internal void PreUpdateForInteractions( Vector2 mouseScrPos ) {
			foreach( HUDElement elem in this.Elements.Values ) {
				if( elem.UpdateEditModeInteractionsWithinEntireUI() ) {
					break;
				}
			}

			if( !HUDElementsLibAPI.IsEditModeActive() ) {
				bool hasInteracted = false;

				foreach( HUDElement elem in this.Elements.Values ) {
					hasInteracted = this.UpdateInteractionsWithinEntireUI_If( elem, mouseScrPos );

					if( hasInteracted ) {
						break;
					}
				}

				if( !hasInteracted ) {
					this.ClearInteractionsIfAny( mouseScrPos );
				}
			}
		}
	}
}