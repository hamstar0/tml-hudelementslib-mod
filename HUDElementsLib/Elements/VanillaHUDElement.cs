using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;


namespace HUDElementsLib {
	public partial class VanillaHUDElement : HUDElement {
		public static IReadOnlyDictionary<string, (Func<bool> context, Rectangle area)> VanillaHUDInfo { get; private set; }



		////////////////

		public Func<bool> EnablingCondition;



		////////////////

		internal VanillaHUDElement( string name, Func<bool> enablingCondition, Rectangle area )
					: base( name, new Vector2(area.X, area.Y), new Vector2(area.Width, area.Height) ) {
			this.EnablingCondition = enablingCondition;
		}


		////////////////

		public override bool SkipSave() => true;

		public override bool IsAnchored() => true;

		public override bool IsLocked() => true;

		public override bool IsEnabled() => this.EnablingCondition.Invoke();
	}
}