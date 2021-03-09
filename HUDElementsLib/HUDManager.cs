using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;


namespace HUDElementsLib {
	partial class HUDManager {
		//internal IDictionary<string, ISet<HUDElement>> Elements = new Dictionary<string, ISet<HUDElement>>();
		public IDictionary<string, HUDElement> Elements = new Dictionary<string, HUDElement>();
		public IDictionary<string, Vector2> SavedElementInfo = new Dictionary<string, Vector2>();

		private UIState MyUI;



		////////////////

		public HUDManager( UIState myUI ) {
			this.MyUI = myUI;
		}


		////////////////
		
		public void LoadHUDElementInfo( string name, float x, float y ) {
			var pos = new Vector2( x, y );

			if( this.Elements.ContainsKey(name) ) {
				this.Elements[name].SetCustomPosition( pos, false );
				this.Elements[name].Recalculate();
			} else {
				this.SavedElementInfo[name] = pos;
			}
		}

		public void LoadHUDElement( HUDElement element ) {
			this.Elements[ element.Name ] = element;

			if( this.SavedElementInfo.ContainsKey(element.Name) ) {
				Vector2 elemInfo = this.SavedElementInfo[ element.Name ];

				this.SavedElementInfo.Remove( element.Name );

				element.SetCustomPosition( elemInfo, false );
				element.Recalculate();
			}
		}
	}
}