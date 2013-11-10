using System;
using Microsoft.Xna.Framework;

namespace IhnLib {
    /// <summary>
    /// Stores data for SystemFollow
    /// </summary>
	[Serializable]
	public class ComponentFollow : Component{
        /// <summary>
        /// Entity this should follow
        /// </summary>
		public Entity ToFollow;
        /// <summary>
        /// Offset to the other entities position
        /// </summary>
		public Position Offset;

        /// <summary>
        /// Instantiates ComponentFollow
        /// </summary>
        /// <param name="toFollow">Entity this should follow</param>
        /// <param name="offset">Offset to the other entities position</param>
		public ComponentFollow(Entity toFollow, Position offset) {
			if(!toFollow.HasComp<ComponentPosition>()) {
				Console.WriteLine("WARNING: Trying to follow component without position");
			}
			ToFollow = toFollow;
			Offset = offset;
		}
        /// <summary>
        /// Instantiates ComponentFollow
        /// </summary>
        /// <param name="toFollow">Entity this should follow</param>
		public ComponentFollow(Entity toFollow) : this(toFollow, new Position(0, 0)) { }
	}
}

