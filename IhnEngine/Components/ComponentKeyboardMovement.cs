//
using System;

namespace IhnLib {
    /// <summary>
    /// Stores data for SystemKeyboardMovement
    /// </summary>
	[Serializable]
	public class ComponentKeyboardMovement : Component{
        /// <summary>
        /// Base speed
        /// </summary>
		public float Speed;
        /// <summary>
        /// Vertical multiplier for speed, North+South
        /// </summary>
		public float VerticalModifier = 1f;
        /// <summary>
        /// Horizontal multiplier for speed, East+West
        /// </summary>
		public float HorizontalModifier = 1f;
        /// <summary>
        /// Instantiates ComponentKeyboardMovement
        /// </summary>
        /// <param name="speed">Speed to move at</param>
		public ComponentKeyboardMovement(float speed = 1) {
			Speed = speed;
		}
	}
}

