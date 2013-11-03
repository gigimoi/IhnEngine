using System;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace IhnLib {
	public static class KeyHelper {
		static Keys[] current = new Keys[0];
		static Keys[] last = new Keys[0];

		public static List<Keys> KeysPressed() {
			List<Keys> fin = new List<Keys>();
			for (int i = 0; i < current.Length; i++) {
				if(KeyPressed(current[i])) {
					fin.Add(current[i]);
				}
			}
			return fin;
		}
		public static bool KeyPressed(Keys k) {
			return !lKeyDown(k) && KeyDown(k);
		}
		public static bool KeyReleased(Keys k) {
			return lKeyDown(k) && !KeyDown(k);
		}
		private static bool lKeyDown(Keys k) {
			for(int i = 0; i < last.Length; i++) {
				if(last[i] == k) {
					return true;
				}
			}
			return false;
		}
		public static bool KeyDown(Keys k) {
			for(int i = 0; i < current.Length; i++) {
				if(current[i] == k) {
					return true;
				}
			}
			return false;
		}
		public static void Update() {
			last = (Keys[])current.Clone();
			current = Keyboard.GetState().GetPressedKeys();
		}
	}
}

