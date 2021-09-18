using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;


namespace HUDElementsLib.Elements.Samples {
	public partial class TimerHUD : HUDElement {
		public delegate long TimeTicker( long currentTicks, out Color renderColor );



		////////////////
		
		private long CurrentTicks = 0;

		////

		private Action PerTickAction;


		////////////////

		public UIPanel ContainerElem { get; private set; }
		public UITextPanel<string> TitleElem { get; private set; }
		public UITextPanel<string> TimerElem { get; private set; }

		////

		public string TitleText { get; private set; }

		public bool ShowTicks { get; private set; }

		////

		public TimeTicker Ticker { get; private set; }

		public Func<bool> Enabler { get; private set; }



		////////////////

		public TimerHUD(
					Vector2 pos,
					Vector2 dim,
					string title,
					long startTimeTicks,
					bool showTicks,
					Func<bool> enabler,
					TimeTicker ticker )
					: base( "Timer", pos, dim )  {
			this.TitleText = title;
			this.CurrentTicks = startTimeTicks;
			this.ShowTicks = showTicks;
			this.Enabler = enabler;
			this.Ticker = ticker;

			this.PerTickAction = () => {
				this.CurrentTicks = this.Ticker.Invoke( this.CurrentTicks, out _ );
			};
		}

		public TimerHUD(
					Vector2 pos,
					Vector2 dim,
					string title,
					long startTimeTicks,
					bool showTicks,
					Func<bool> enabler,
					bool isCountdownOrElseClock = true )
					: base( "Timer", pos, dim )  {
			this.TitleText = title;
			this.CurrentTicks = startTimeTicks;
			this.ShowTicks = showTicks;
			this.Enabler = enabler;

			if( isCountdownOrElseClock ) {
				this.Ticker = TimerHUD.DefaultCountdownTicker;
			} else {
				this.Ticker = TimerHUD.DefaultClockTicker;
			}

			this.PerTickAction = () => {
				this.CurrentTicks = this.Ticker.Invoke( this.CurrentTicks, out _ );
			};
		}


		////////////////
		
		public override bool IsEnabled() {
			return this.Enabler.Invoke();
		}


		////////////////
		
		public void SetTimerTicks( long ticks ) {
			this.CurrentTicks = ticks;
		}


		////////////////
		
		public bool StartTimer() {
			var mymod = HUDElementsLibMod.Instance;

			return mymod.PerTickActions.Add( this.PerTickAction );
		}

		public bool PauseTimer() {
			var mymod = HUDElementsLibMod.Instance;

			return mymod.PerTickActions.Remove( this.PerTickAction );
		}


		////////////////

		public override void Update( GameTime gameTime ) {
			base.Update( gameTime );
		}
	}
}