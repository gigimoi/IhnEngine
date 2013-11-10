using System;

namespace IhnLib {
    /// <summary>
    /// Stores data for SystemGravity
    /// </summary>
	[Serializable]
	public class ComponentGravity : Component{
        /// <summary>
        /// Static value to multiply all gravity
        /// </summary>
		public static float GlobalMultiplier = 1f;
        /// <summary>
        /// Gravity multiplier for this component
        /// </summary>
		public float Multiplier = 1;
        /// <summary>
        /// Instantiates ComponentGravity
        /// </summary>
        /// <param name="multiplier">Multiplier to use</param>
		public ComponentGravity(float multiplier = 1) {
			Multiplier = multiplier;
		}
	}
}