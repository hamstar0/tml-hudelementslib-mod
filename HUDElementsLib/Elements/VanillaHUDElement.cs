using System;
using Microsoft.Xna.Framework;
using Terraria;


namespace HUDElementsLib {
	public struct VanillaHUDElementDefinition {
		public string Name;
		public Func<bool> Context;
		public Func<Vector2> PositionWithAnchors;
		public Func<Vector2> Dimensions;
		public Func<Vector2> Displacement;

		public VanillaHUDElementDefinition(
					string name,
					Func<bool> context,
					Func<Vector2> position,
					Func<Vector2> dimensions,
					Func<Vector2> displacement = null ) {
			this.Name = name;
			this.Context = context;
			this.PositionWithAnchors = position;
			this.Dimensions = dimensions;
			this.Displacement = displacement;
		}
	}




	public partial class VanillaHUDElement : HUDElement {
		public static VanillaHUDElementDefinition[] VanillaHUDInfo { get; private set; }



		////////////////

		public Func<bool> EnablingCondition { get; private set; }
		private Func<Vector2> DynamicCustomPositionAndAnchors;
		private Func<Vector2> DynamicCustomDimensions;
		private Func<Vector2> DisplacementOverride;



		////////////////

		internal VanillaHUDElement( VanillaHUDElementDefinition def ) : base( def.Name, def.PositionWithAnchors(), def.Dimensions() ) {
			this.EnablingCondition = def.Context;
			this.DynamicCustomPositionAndAnchors = def.PositionWithAnchors;
			this.DynamicCustomDimensions = def.Dimensions;
			this.DisplacementOverride = def.Displacement;
		}


		////////////////

		public override bool SkipSave() => true;

		public override bool IsLocked() => true;

		public override bool IsEnabled() => this.EnablingCondition.Invoke();


		////////////////
		
		public override Vector2 GetHUDComputedPosition( bool applyDisplacement ) {
			this.CustomPositionWithAnchor = this.DynamicCustomPositionAndAnchors();
			return base.GetHUDComputedPosition( applyDisplacement );
		}

		public override Vector2 GetHUDComputedDimensions() {
			this.CustomDimensions = this.DynamicCustomDimensions();
			return base.GetHUDComputedDimensions();
		}


		////////////////

		public override Vector2 GetDisplacementDirection( HUDElement against ) {
			if( this.DisplacementOverride != null ) {
				return this.DisplacementOverride();
			}
			return base.GetDisplacementDirection( against );
		}
	}
}