using System;
using Microsoft.Xna.Framework;

namespace IhnLib {
    /// <summary>
    /// Serializable version of XNA's rectangle that also supports floating points
    /// </summary>
	[Serializable]
	public class FloatRect {
        /// <summary>
        /// XCoord
        /// </summary>
		public float X;
        /// <summary>
        /// YCoord
        /// </summary>
		public float Y;
        /// <summary>
        /// Width
        /// </summary>
		public float Width;
        /// <summary>
        /// Height
        /// </summary>
		public float Height;
        /// <summary>
        /// Instantiates a new FloatRect
        /// </summary>
        /// <param name="x">XCoord</param>
        /// <param name="y">YCoord</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
		public FloatRect(float x, float y, float w, float h) {
			X = x;
			Y = y;
			Width = w;
			Height = h;
		}
        /// <summary>
        /// Converts back to an XNA rectangle
        /// </summary>
        /// <returns>XNA rectangle</returns>
		public Rectangle ToRect() {
			return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
		}
        /// <summary>
        /// Gets a string representation of the rectangle's data
        /// </summary>
        /// <returns>String representing rectangle's data</returns>
		public override string ToString() {
			return "{" + "X:" + X + ",Y:" + Y + ",W:" + Width + ",H:" + Height + "}";
		}
	}
}

