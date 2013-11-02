//
// MouseHelper.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using Microsoft.Xna.Framework.Input;

namespace IhnLib {
	public static class MouseHelper {
		static bool Left = false;
		static bool Right = false;
		static bool Middle = false;
		public static int X = 0;
		public static int Y = 0;
		static bool lastLeft = false;
		static bool lastRight = false;
		static bool lastMiddle = false;
		static int lastWheel;
		static int wheel;
		public static int WheelDelta;
		public static bool MouseLeftReleased() {
			return !Left && lastLeft;
		}
		public static bool MouseLeftPressed() {
			return Left && !lastLeft;
		}
		public static bool MouseLeftDown() {
			return Left;
		}
		public static bool MouseRightReleased() {
			return !Right && lastRight;
		}
		public static bool MouseRightPressed() {
			return Right && !lastRight;
		}
		public static bool MouseRightDown() {
			return Right;
		}
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

