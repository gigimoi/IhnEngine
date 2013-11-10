using System;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace IhnLib {
    /// <summary>
    /// Aids in keyboard input management
    /// </summary>
	public static class KeyHelper {
		static Keys[] current = new Keys[0];
		static Keys[] last = new Keys[0];

        /// <summary>
        /// Gets all keys just pressed
        /// </summary>
        /// <returns>List of keys currently pressed</returns>
		public static List<Keys> KeysPressed() {
			List<Keys> fin = new List<Keys>();
			for (int i = 0; i < current.Length; i++) {
				if(KeyPressed(current[i])) {
					fin.Add(current[i]);
				}
			}
			return fin;
		}
        /// <summary>
        /// Checks if a key was just pressed
        /// </summary>
        /// <param name="k">Key to check</param>
        /// <returns>If the key was just pressed</returns>
		public static bool KeyPressed(Keys k) {
			return !lKeyDown(k) && KeyDown(k);
		}
        /// <summary>
        /// Checks if a key was just released
        /// </summary>
        /// <param name="k">Key to check</param>
        /// <returns>If the key was just released</returns>
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
        /// <summary>
        /// Checks if a key is down
        /// </summary>
        /// <param name="k">Key to check</param>
        /// <returns>If the key is down</returns>
		public static bool KeyDown(Keys k) {
			for(int i = 0; i < current.Length; i++) {
				if(current[i] == k) {
					return true;
				}
			}
			return false;
		}
        /// <summary>
        /// Updates keyboard status, automatically called by ihn
        /// </summary>
		public static void Update() {
			last = (Keys[])current.Clone();
			current = Keyboard.GetState().GetPressedKeys();
		}
	}
}

