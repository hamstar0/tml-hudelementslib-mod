using System;
using Microsoft.Xna.Framework;
using Terraria;


namespace HUDElementsLib {
	public struct VanillaHUDElementDefinition {
		public string Name;
		public Func<bool> Context;
		public Func<Vector2> Position;
		public Func<Vector2> Dimensions;

		public VanillaHUDElementDefinition( string name, Func<bool> context, Func<Vector2> position, Func<Vector2> dimensions ) {
			this.Name = name;
			this.Context = context;
			this.Position = position;
			this.Dimensions = dimensions;
		}
	}




	public partial class VanillaHUDElement : HUDElement {
		public static VanillaHUDElementDefinition[] VanillaHUDInfo { get; private set; }



		////////////////

		public Func<bool> EnablingCondition { get; private set; }
		private Func<Vector2> DynamicCustomPosition;
		private Func<Vector2> DynamicCustomDimensions;



		////////////////

		internal VanillaHUDElement( VanillaHUDElementDefinition def ) : base( def.Name, def.Position(), def.Dimensions() ) {
			this.EnablingCondition = def.Context;
			this.DynamicCustomPosition = def.Position;
			this.DynamicCustomDimensions = def.Dimensions;
		}


		////////////////

		public override bool SkipSave() => true;

		public override bool IsLocked() => true;

		public override bool IsEnabled() => this.EnablingCondition.Invoke();


		////////////////

		public override Vector2 GetPositionOnHUD( bool withoutDisplacement ) {
			this.CustomPosition = this.DynamicCustomPosition();
			return base.GetPositionOnHUD( withoutDisplacement );
		}

		public override Vector2 GetDimensionsOnHUD() {
			this.CustomDimensions = this.DynamicCustomDimensions();
			return base.GetDimensionsOnHUD();
		}
	}
}