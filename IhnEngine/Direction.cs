using System;

namespace IhnLib {
    /// <summary>
    /// Enum for all ordinal(cardinal?) directions
    /// </summary>
	public enum Direction {
        /// <summary>
        /// Up
        /// </summary>
		North,
        /// <summary>
        /// Up and to the right
        /// </summary>
		NorthEast,
        /// <summary>
        /// Right
        /// </summary>
		East,
        /// <summary>
        /// Down and to the right
        /// </summary>
		SouthEast,
        /// <summary>
        /// Down
        /// </summary>
		South,
        /// <summary>
        /// Down and to the left
        /// </summary>
		SouthWest,
        /// <summary>
        /// Left
        /// </summary>
		West,
        /// <summary>
        /// Up and tp the left
        /// </summary>
		NorthWest,
        /// <summary>
        /// No direction
        /// </summary>
		Center
	}
}

