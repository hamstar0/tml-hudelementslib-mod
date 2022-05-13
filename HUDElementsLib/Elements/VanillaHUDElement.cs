using System;
using Microsoft.Xna.Framework;
using Terraria;


namespace HUDElementsLib.Elements {
	public struct VanillaHUDElementDefinition {
		public string Name;
		public Func<bool> Context;
		public Func<(Vector2 relPos, Vector2 percPos)> Position;
		public Func<Vector2> Dimensions;
		public Func<Vector2> Displacement;

		public VanillaHUDElementDefinition(
					string name,
					Func<bool> context,
					Func<(Vector2 relPos, Vector2 percPos)> position,
					Func<Vector2> dimensions,
					Func<Vector2> displacement = null ) {
			this.Name = name;
			this.Context = context;
			this.Position = position;
			this.Dimensions = dimensions;
			this.Displacement = displacement;
		}
	}




	public partial class VanillaHUDElement : HUDElement {
		public static VanillaHUDElementDefinition[] VanillaHUDInfo { get; private set; }



		////////////////

		private Func<(Vector2 relPos, Vector2 percPos)> DynamicIntendedPosition;
		private Func<Vector2> DynamicIntendedDimensions;
		private Func<Vector2> DisplacementOverride;



		////////////////

		internal VanillaHUDElement( VanillaHUDElementDefinition def )
					: base( def.Name, def.Position().relPos, def.Position().percPos, def.Dimensions(), def.Context ) {
			this.DynamicIntendedPosition = def.Position;
			this.DynamicIntendedDimensions = def.Dimensions;
			this.DisplacementOverride = def.Displacement;
		}


		////////////////

		public override bool SkipSave() => true;

		public override bool IsDragLocked() => true;

		public override bool IsAnchorsToggleable() => false;

		public override bool AutoAnchors() => false;


		////////////////
		
		public override Vector2 GetHUDComputedPosition( bool applyDisplacement ) {
			(Vector2 relPos, Vector2 percPos) = this.DynamicIntendedPosition();

			this.CurrentRelativePosition = relPos;
			this.CurrentPositionPercent = percPos;

			return base.GetHUDComputedPosition( applyDisplacement );
		}

		public override Vector2 GetHUDComputedDimensions() {
			this.CurrentDimensions = this.DynamicIntendedDimensions();
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