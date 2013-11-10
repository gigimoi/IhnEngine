using System;

namespace IhnLib {
    /// <summary>
    /// Indicates that an entity has a hitbox using width, height, xcoord, and ycoord
    /// </summary>
	[Serializable]
	public class ComponentAABB : Component{
        /// <summary>
        /// Width of the hitbox
        /// </summary>
		public int Width;
        /// <summary>
        /// Height of the hitbox
        /// </summary>
		public int Height;
        /// <summary>
        /// XCoord of the hitbox
        /// </summary>
		public int X;
        /// <summary>
        /// YCoord of the hitbox
        /// </summary>
		public int Y;
        /// <summary>
        /// Constructs a new hitbox
        /// </summary>
        /// <param name="x">XCoord</param>
        /// <param name="y">YCoord</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
		public ComponentAABB(int x, int y, int w, int h) {
			Width = w;
			Height = h;
			X = x;
			Y = y;
		}
	}
}

