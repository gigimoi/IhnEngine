//
// ComponentHealth.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;

namespace IhnLib {
	[Serializable]
	public class ComponentHealth : Component{
		public ComponentHealth(int maxHealth) {
			MaxHealth = maxHealth;
			Health = maxHealth;
		}
		public int Health;
		public int MaxHealth;
	}
}

