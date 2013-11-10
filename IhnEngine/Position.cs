using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
    /// <summary>
    /// Serializable version of a vector
    /// </summary>
	[Serializable]
	public class Position {
        /// <summary>
        /// Coords of the vector
        /// </summary>
		public float X, Y;
        /// <summary>
        /// Instantiates a position with coords
        /// </summary>
        /// <param name="x">XCoord</param>
        /// <param name="y">YCoord</param>
		public Position(float x, float y) {
			X = x;
			Y = y;
		}
        /// <summary>
        /// Converts position to a vector
        /// </summary>
        /// <returns>Vector equal to position</returns>
		public Vector2 ToVector() {
			return new Vector2(X, Y);
		}
	}
}
