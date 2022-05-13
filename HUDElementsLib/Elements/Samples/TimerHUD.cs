using System;
using Microsoft.Xna.Framework;
using Terraria;


namespace HUDElementsLib.Elements.Samples {
	public partial class TimerHUD : HUDElement {
		public delegate long TimeTicker( long currentTicks, out Color renderColor );



		////////////////
		
		private long CurrentTicks = 0;

		////

		private Action PerTickAction;


		////

		public float Scale = 1f;

		public Color TitleColor = Color.White;


		////////////////

		public string TitleText { get; private set; }

		public bool ShowTicks { get; private set; }

		////

		public TimeTicker Ticker { get; private set; }



		////////////////

		public TimerHUD(
					Vector2 relPos,
					Vector2 percPos,
					Vector2 dim,
					string title,
					long startTimeTicks,
					bool showTicks,
					Func<bool> enabler,
					TimeTicker ticker )
					: base( "Timer_"+title, relPos, percPos, dim, enabler )  {
			this.BasicConstructor( title, startTimeTicks, showTicks, ticker );
		}

		public TimerHUD(
					Vector2 relPos,
					Vector2 percPos,
					Vector2 dim,
					string title,
					long startTimeTicks,
					bool showTicks,
					Func<bool> enabler,
					bool isCountdownOrElseClock = true )
					: base( "Timer_"+title, relPos, percPos, dim, enabler )  {
			TimeTicker ticker;
			if( isCountdownOrElseClock ) {
				ticker = TimerHUD.DefaultCountdownTicker;
			} else {
				ticker = TimerHUD.DefaultClockTicker;
			}

			this.BasicConstructor( title, startTimeTicks, showTicks, ticker );
		}

		////

		private void BasicConstructor(
					string title,
					long startTimeTicks,
					bool showTicks,
					TimeTicker ticker ) {
			this.TitleText = title;
			this.CurrentTicks = startTimeTicks;
			this.ShowTicks = showTicks;
			this.Ticker = ticker;

			this.PerTickAction = () => {
				this.CurrentTicks = this.Ticker.Invoke( this.CurrentTicks, out _ );
			};
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
	}
}