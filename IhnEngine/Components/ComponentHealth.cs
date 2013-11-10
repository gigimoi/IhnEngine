//
// ComponentHealth.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;

namespace IhnLib {
    /// <summary>
    /// Stores data for various systems using health/hit points
    /// </summary>
	[Serializable]
	public class ComponentHealth : Component{
        /// <summary>
        /// Instantiates ComponentHealth
        /// </summary>
        /// <param name="maxHealth">Maximum health possible for entity</param>
		public ComponentHealth(int maxHealth) {
			MaxHealth = maxHealth;
			Hp = maxHealth;
		}
        /// <summary>
        /// Current health
        /// </summary>
		public int Hp;
        /// <summary>
        /// Maximum health possible
        /// </summary>
		public int MaxHealth;
	}
}

