using System;

namespace IhnLib {
    /// <summary>
    /// Component containing a direction
    /// </summary>
	[Serializable]
	public class ComponentDirection : Component{
        /// <summary>
        /// Direction of this component
        /// </summary>
		public Direction Dir;
        /// <summary>
        /// Creates a new component direction facing dir
        /// </summary>
        /// <param name="dir">Direction to face</param>
		public ComponentDirection(Direction dir) {
			Dir = dir;
		}
	}
}

