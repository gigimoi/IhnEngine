using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
	[Serializable]
	public class Position {
		public float X, Y;
		public Position(float x, float y) {
			X = x;
			Y = y;
		}
		public Vector2 ToVector() {
			return new Vector2(X, Y);
		}
	}
}
