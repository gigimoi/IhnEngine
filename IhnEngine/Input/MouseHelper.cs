using System;
using Microsoft.Xna.Framework.Input;

namespace IhnLib {
    /// <summary>
    /// Aids in mouse control
    /// </summary>
	public static class MouseHelper {
		static bool Left = false;
		static bool Right = false;
		static bool Middle = false;
        /// <summary>
        /// XCoord of the mouse relative to window
        /// </summary>
		public static int X = 0;
        /// <summary>
        /// YCoord of the mouse relative to window
        /// </summary>
		public static int Y = 0;
		static bool lastLeft = false;
		static bool lastRight = false;
		static bool lastMiddle = false;
		static int lastWheel;
		static int wheel;
        /// <summary>
        /// The value that the mousewheel has changed by
        /// </summary>
		public static int WheelDelta;
        /// <summary>
        /// Checks if the left mouse button was just released
        /// </summary>
        /// <returns>If the left mouse button was just released</returns>
		public static bool MouseLeftReleased() {
			return !Left && lastLeft;
		}
        /// <summary>
        /// Checks if the left mouse button was just pressed
        /// </summary>
        /// <returns>If the left mouse button was just pressed</returns>
		public static bool MouseLeftPressed() {
			return Left && !lastLeft;
		}
        /// <summary>
        /// Checks if the left mouse button is down
        /// </summary>
        /// <returns>Left mouse button down?</returns>
		public static bool MouseLeftDown() {
			return Left;
		}
        /// <summary>
        /// Checks if the right mouse button was just released
        /// </summary>
        /// <returns>If the right mouse button was just released</returns>
		public static bool MouseRightReleased() {
			return !Right && lastRight;
		}
        /// <summary>
        /// Checks if the right mouse button was just pressed
        /// </summary>
        /// <returns>If the right mouse button was just pressed</returns>
		public static bool MouseRightPressed() {
			return Right && !lastRight;
		}
        /// <summary>
        /// Checks if the right mouse button is down
        /// </summary>
        /// <returns>Right mouse button down?</returns>
		public static bool MouseRightDown() {
			return Right;
		}
        /// <summary>
        /// Updates mouse status, automaticlaly called by ihn
        /// </summary>
		public static void Update() {
			var ms = Mouse.GetState();
			lastLeft = Left;
			lastRight = Right;
			lastMiddle = Middle;
			Left = ms.LeftButton == ButtonState.Pressed;
			Right = ms.RightButton == ButtonState.Pressed;
			Middle = ms.MiddleButton == ButtonState.Pressed;
			X = (int)(ms.X / Ihn.Instance.Zoom);
			Y = (int)(ms.Y / Ihn.Instance.Zoom);
			lastWheel = wheel;
			wheel = ms.ScrollWheelValue;
			WheelDelta = wheel - lastWheel;
		}
	}
}

