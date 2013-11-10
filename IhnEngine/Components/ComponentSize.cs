using System;

namespace IhnLib {
    /// <summary>
    /// Width/Height component for entity
    /// </summary>
	[Serializable]
	public class ComponentSize : Component{
        /// <summary>
        /// Width of entity
        /// </summary>
		public int Width;
        /// <summary>
        /// Height of entity
        /// </summary>
		public int Height;
        /// <summary>
        /// Creates a new ComponentSize
        /// </summary>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
		public ComponentSize(int width, int height) {
			Width = width;
			Height = height;
		}
	}
}

