using System;
using Microsoft.Xna.Framework;
using Terraria;


namespace HUDElementsLib {
	public struct VanillaHUDElementDefinition {
		public string Name;
		public Func<bool> Context;
		public Func<Vector2> PositionWithAnchors;
		public Func<Vector2> Dimensions;

		public VanillaHUDElementDefinition( string name, Func<bool> context, Func<Vector2> position, Func<Vector2> dimensions ) {
			this.Name = name;
			this.Context = context;
			this.PositionWithAnchors = position;
			this.Dimensions = dimensions;
		}
	}




	public partial class VanillaHUDElement : HUDElement {
		public static VanillaHUDElementDefinition[] VanillaHUDInfo { get; private set; }



		////////////////

		public Func<bool> EnablingCondition { get; private set; }
		private Func<Vector2> DynamicCustomPositionAndAnchors;
		private Func<Vector2> DynamicCustomDimensions;



		////////////////

		internal VanillaHUDElement( VanillaHUDElementDefinition def ) : base( def.Name, def.PositionWithAnchors(), def.Dimensions() ) {
			this.EnablingCondition = def.Context;
			this.DynamicCustomPositionAndAnchors = def.PositionWithAnchors;
			this.DynamicCustomDimensions = def.Dimensions;
		}


		////////////////

		public override bool SkipSave() => true;

		public override bool IsLocked() => true;

		public override bool IsEnabled() => this.EnablingCondition.Invoke();


		////////////////

		public override Vector2 GetHudComputedPosition( bool withoutDisplacement ) {
			this.CustomPositionWithAnchor = this.DynamicCustomPositionAndAnchors();
			return base.GetHudComputedPosition( withoutDisplacement );
		}

		public override Vector2 GetHudComputedDimensions() {
			this.CustomDimensions = this.DynamicCustomDimensions();
			return base.GetHudComputedDimensions();
		}
	}
}