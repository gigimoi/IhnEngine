using System;

namespace IhnLib {
    /// <summary>
    /// Component to store X/Y coords
    /// </summary>
	[Serializable]
	public class ComponentPosition : Component{
        /// <summary>
        /// Instantiates ComponentPosition
        /// </summary>
        /// <param name="x">XCoord</param>
        /// <param name="y">YCoord</param>
		public ComponentPosition(float x, float y) {
			X = x;
			Y = y;
		}
        /// <summary>
        /// XCoord
        /// </summary>
		public float X;
        /// <summary>
        /// YCoord
        /// </summary>
		public float Y;
	}
}

