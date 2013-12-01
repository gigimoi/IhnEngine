using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace IhnLib {
    /// <summary>
    /// Component to store data used by zombie ais
    /// </summary>
	[Serializable]
	public class ComponentZombieAI : Component{
        /// <summary>
        /// Managed variable that forces path refresh when true
        /// </summary>
		public bool ShouldRefreshPath;
        /// <summary>
        /// Current path to follow
        /// </summary>
		public List<Position>Path;
        /// <summary>
        /// Where is the zombie heading to XCoord
        /// </summary>
		public int ChaseX;
        /// <summary>
        /// Where is the zombie heading to YCoord
        /// </summary>
		public int ChaseY;
        /// <summary>
        /// Zombie speed
        /// </summary>
		public float Speed;
        /// <summary>
        /// Tilesize to round to when checking paths
        /// </summary>
		public int TileSize;
        /// <summary>
        /// Current distance along the path
        /// </summary>
		public int PathStep;
        /// <summary>
        /// Time taken to swipe
        /// </summary>
		public int MaxSwipeTime;
        /// <summary>
        /// Swipe backswing time
        /// </summary>
		public int SwipeTime;
        /// <summary>
        /// Instantiates a new zombie ai component
        /// </summary>
        /// <param name="speed">Speed to move at</param>
        /// <param name="tileSize">Size of tiles</param>
        /// <param name="swipeTime">Time taken to swipe</param>
		public ComponentZombieAI(float speed, int tileSize, int swipeTime) {
			Speed = speed;
			TileSize = tileSize;
			Path = new List<Position>();
			MaxSwipeTime = swipeTime;
		}
	}
}

