using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace HUDElementsLib {
	public partial class HUDElementsLibConfig : ModConfig {
		public static HUDElementsLibConfig Instance => ModContent.GetInstance<HUDElementsLibConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////
		
		[DefaultValue( true )]
		public bool EnableCollisionsToggleControl { get; set; } = true;


		[DefaultValue( false )]
		public bool EnableAnchorsToggleControl { get; set; } = false;	// TODO: Implement auto-anchoring toggle button
	}
}
