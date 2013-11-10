//
using System;

namespace IhnLib {
    /// <summary>
    /// Component giving entity a radius
    /// </summary>
	[Serializable]
	public class ComponentRadius : Component{
        /// <summary>
        /// Radius size
        /// </summary>
		public int Radius;
        /// <summary>
        /// Instantiates ComponentRadius
        /// </summary>
        /// <param name="radius">Length of radius</param>
		public ComponentRadius(int radius) {
			Radius = radius;
		}
	}
}

