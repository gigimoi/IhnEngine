using System;

namespace IhnLib {
    /// <summary>
    /// Component to store velocity
    /// </summary>
	[Serializable]
	public class ComponentVelocity : Component{
        /// <summary>
        /// Instantiates a new velocity of (0, 0)
        /// </summary>
		public ComponentVelocity() : this(0, 0) { }
        /// <summary>
        /// Instantiates a new velocity of (x, y)
        /// </summary>
        /// <param name="x">XSpeed</param>
        /// <param name="y">YSpeed</param>
		public ComponentVelocity(float x, float y) {
			X = x;
			Y = y;
		}
        /// <summary>
        /// XSpeed
        /// </summary>
		public float X;
        /// <summary>
        /// YSpeed
        /// </summary>
		public float Y;
	}
}

