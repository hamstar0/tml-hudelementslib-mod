using System;
using Microsoft.Xna.Framework;
using Terraria;


namespace HUDElementsLib.Elements.Samples {
	public partial class CompletionStatHUD : HUDElement {
		public delegate (int completed, int total) StatGetter();



		////////////////
		
		public string TitleText { get; private set; }

		////

		public StatGetter Stat { get; private set; }

		////

		public Func<bool> Enabler { get; private set; }



		////////////////

		public CompletionStatHUD(
					Vector2 pos,
					Vector2 dim,
					string title,
					Func<bool> enabler,
					StatGetter stat ) : base( "Stat_"+title, pos, dim )  {
				this.TitleText = title;
			this.Enabler = enabler;
			this.Stat = stat;
		}


		////////////////

		public override bool IsEnabled() {
			return this.Enabler.Invoke();
		}
	}
}