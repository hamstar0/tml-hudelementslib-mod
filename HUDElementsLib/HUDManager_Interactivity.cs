using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;


namespace HUDElementsLib {
	partial class HUDManager {
		private double _ClickDisabledMillisecondsRemaining;
		private double _LastMouseDownMilliseconds;
		private double _LastMouseRightDownMilliseconds;
		private double _LastMouseMiddleDownMilliseconds;
		private double _LastMouseXButton1DownMilliseconds;
		private double _LastMouseXButton2DownMilliseconds;
		private HUDElement _LastElementHover;
		private HUDElement _LastElementLeftDown;
		private HUDElement _LastElementLeftClicked;
		private HUDElement _LastElementRightDown;
		private HUDElement _LastElementRightClicked;
		private HUDElement _LastElementMiddleDown;
		private HUDElement _LastElementMiddleClicked;
		private HUDElement _LastElementXButton1Down;
		private HUDElement _LastElementXButton1Clicked;
		private HUDElement _LastElementXButton2Down;
		private HUDElement _LastElementXButton2Clicked;
		private bool _WasMouseLeftDown;
		private bool _WasMouseRightDown;
		private bool _WasMouseMiddleDown;
		private bool _WasMouseXButton1Down;
		private bool _WasMouseXButton2Down;



		////////////////

		private bool UpdateInteractionsWithinEntireUI_If( HUDElement elem ) {
			if( !elem.IsEnabled() ) {
				return false;
			}
			if( !elem.GetHUDComputedArea(true).Contains(Main.mouseX, Main.mouseY) ) {
				return false;
			}

			GameTime time = Main._drawInterfaceGameTime;
			if( time == null ) {
				return false;
			}

			this._ClickDisabledMillisecondsRemaining = Math.Max(
				0.0d,
				this._ClickDisabledMillisecondsRemaining - time.ElapsedGameTime.TotalMilliseconds
			);

			if( this._ClickDisabledMillisecondsRemaining > 0.0d ) {
				return true;
			}

			//

			var mousePos = new Vector2( (float)Main.mouseX, (float)Main.mouseY );
			bool mouseLeftDown = Main.mouseLeft; //&& Main.hasFocus;
			bool mouseRightDown = Main.mouseRight; //&& Main.hasFocus;
			bool mouseMiddleDown = Main.mouseMiddle; //&& Main.hasFocus;
			bool mouseXButton1Down = Main.mouseXButton1; //&& Main.hasFocus;
			bool mouseXButton2Down = Main.mouseXButton2; //&& Main.hasFocus;

			//

			if( elem != this._LastElementHover ) {
				if( this._LastElementHover != null ) {
					this._LastElementHover.MouseOut( new UIMouseEvent(this._LastElementHover, mousePos) );
				}

				elem.MouseOver( new UIMouseEvent(elem, mousePos) );

				this._LastElementHover = elem;
			}

			//

//ModLibsCore.Libraries.Debug.DebugLibraries.Print( "hud_interact_"+elem.Name, "mld:"+mouseLeftDown+", wmld:"+this._WasMouseLeftDown+", led:"+this._LastElementDown );
			if( mouseLeftDown && !this._WasMouseLeftDown ) {
				this._LastElementLeftDown = elem;

				elem.MouseDown( new UIMouseEvent( elem, mousePos ) );

				double milliSinceLastMouseDown = time.TotalGameTime.TotalMilliseconds - this._LastMouseDownMilliseconds;

				if( this._LastElementLeftClicked == elem && milliSinceLastMouseDown < 500.0 ) {
					elem.DoubleClick( new UIMouseEvent( elem, mousePos ) );
					this._LastElementLeftClicked = null;
				}

				this._LastMouseDownMilliseconds = time.TotalGameTime.TotalMilliseconds;
			} else if( this._LastElementLeftDown != null ) {
				if( this._LastElementLeftDown.GetHUDComputedArea(true).Contains(Main.mouseX, Main.mouseY) ) {
					this._LastElementLeftDown.Click( new UIMouseEvent(this._LastElementLeftDown, mousePos) );

					this._LastElementLeftClicked = this._LastElementLeftDown;
				}

				this._LastElementLeftDown.MouseUp( new UIMouseEvent(this._LastElementLeftDown, mousePos) );

				this._LastElementLeftDown = null;
			}

			//

			// tModLoader added functionality, right, middle, extra button 1 & 2 click Events
			if( mouseRightDown && !this._WasMouseRightDown ) {
				this._LastElementRightDown = elem;

				elem.RightMouseDown( new UIMouseEvent( elem, mousePos ) );

				double milliSinceLastMouseRightDown = time.TotalGameTime.TotalMilliseconds - this._LastMouseRightDownMilliseconds;

				if( this._LastElementRightClicked == elem && milliSinceLastMouseRightDown < 500.0 ) {
					elem.RightDoubleClick( new UIMouseEvent( elem, mousePos ) );
					this._LastElementRightClicked = null;
				}

				this._LastMouseRightDownMilliseconds = time.TotalGameTime.TotalMilliseconds;
			} else if( this._LastElementRightDown != null ) {
				if( this._LastElementRightDown.GetHUDComputedArea( true ).Contains( Main.mouseX, Main.mouseY ) ) {
					this._LastElementRightDown.RightClick( new UIMouseEvent( this._LastElementRightDown, mousePos ) );

					this._LastElementRightClicked = this._LastElementRightDown;
				}

				this._LastElementRightDown.RightMouseUp( new UIMouseEvent( this._LastElementRightDown, mousePos ) );

				this._LastElementRightDown = null;
			}

			//

			if( mouseMiddleDown && !this._WasMouseMiddleDown ) {
				this._LastElementMiddleDown = elem;

				elem.MiddleMouseDown( new UIMouseEvent( elem, mousePos ) );

				double milliSinceLastMouseMiddleDown = time.TotalGameTime.TotalMilliseconds - this._LastMouseMiddleDownMilliseconds;

				if( this._LastElementMiddleClicked == elem && milliSinceLastMouseMiddleDown < 500.0 ) {
					elem.MiddleDoubleClick( new UIMouseEvent( elem, mousePos ) );

					this._LastElementMiddleClicked = null;
				}

				this._LastMouseMiddleDownMilliseconds = time.TotalGameTime.TotalMilliseconds;
			} else if( this._LastElementMiddleDown != null ) {
				if( this._LastElementMiddleDown.GetHUDComputedArea( true ).Contains( Main.mouseX, Main.mouseY ) ) {
					this._LastElementMiddleDown.MiddleClick( new UIMouseEvent( this._LastElementMiddleDown, mousePos ) );

					this._LastElementMiddleClicked = this._LastElementMiddleDown;
				}

				this._LastElementMiddleDown.MiddleMouseUp( new UIMouseEvent( this._LastElementMiddleDown, mousePos ) );

				this._LastElementMiddleDown = null;
			}

			//

			if( mouseXButton1Down && !this._WasMouseXButton1Down ) {
				this._LastElementXButton1Down = elem;

				elem.XButton1MouseDown( new UIMouseEvent( elem, mousePos ) );

				double milliSinceLastX1Down = time.TotalGameTime.TotalMilliseconds - this._LastMouseXButton1DownMilliseconds;
				
				if( this._LastElementXButton1Clicked == elem && milliSinceLastX1Down < 500.0 ) {
					elem.XButton1DoubleClick( new UIMouseEvent( elem, mousePos ) );

					this._LastElementXButton1Clicked = null;
				}

				this._LastMouseXButton1DownMilliseconds = time.TotalGameTime.TotalMilliseconds;
			} else if( this._LastElementXButton1Down != null ) {
				if( this._LastElementXButton1Down.GetHUDComputedArea( true ).Contains( Main.mouseX, Main.mouseY ) ) {
					this._LastElementXButton1Down.XButton1Click( new UIMouseEvent( this._LastElementXButton1Down, mousePos ) );

					this._LastElementXButton1Clicked = this._LastElementXButton1Down;
				}

				this._LastElementXButton1Down.XButton1MouseUp( new UIMouseEvent( this._LastElementXButton1Down, mousePos ) );

				this._LastElementXButton1Down = null;
			}

			//
			
			if( mouseXButton2Down && !this._WasMouseXButton2Down ) {
				this._LastElementXButton2Down = elem;

				elem.XButton2MouseDown( new UIMouseEvent( elem, mousePos ) );

				double millisSinceLastX2Down = time.TotalGameTime.TotalMilliseconds - this._LastMouseXButton2DownMilliseconds;
				
				if( this._LastElementXButton2Clicked == elem && millisSinceLastX2Down < 500.0 ) {

					elem.XButton2DoubleClick( new UIMouseEvent( elem, mousePos ) );
					this._LastElementXButton2Clicked = null;
				}

				this._LastMouseXButton2DownMilliseconds = time.TotalGameTime.TotalMilliseconds;
			} else if( this._LastElementXButton2Down != null ) {
				if( this._LastElementXButton2Down.GetHUDComputedArea( true ).Contains( Main.mouseX, Main.mouseY ) ) {
					this._LastElementXButton2Down.XButton2Click( new UIMouseEvent( this._LastElementXButton2Down, mousePos ) );

					this._LastElementXButton2Clicked = this._LastElementXButton2Down;
				}

				this._LastElementXButton2Down.XButton2MouseUp( new UIMouseEvent( this._LastElementXButton2Down, mousePos ) );

				this._LastElementXButton2Down = null;
			}

			//

			if( PlayerInput.ScrollWheelDeltaForUI != 0 ) {
				elem.ScrollWheel( new UIScrollWheelEvent( elem, mousePos, PlayerInput.ScrollWheelDeltaForUI ) );
				// PlayerInput.ScrollWheelDeltaForUI = 0; Moved after ModHooks.UpdateUI(gameTime);
			}

			//

			this._WasMouseLeftDown = mouseLeftDown;
			this._WasMouseRightDown = mouseRightDown;
			this._WasMouseMiddleDown = mouseMiddleDown;
			this._WasMouseXButton1Down = mouseXButton1Down;
			this._WasMouseXButton2Down = mouseXButton2Down;

			return true;
		}


		////

		private void ClearInteractionsIfAny() {
			var mousePos = new Vector2( (float)Main.mouseX, (float)Main.mouseY );

			//

			if( this._LastElementHover != null ) {
				this._LastElementHover.MouseOut( new UIMouseEvent( this._LastElementHover, mousePos ) );

				this._LastElementHover = null;
			}
		}
	}
}