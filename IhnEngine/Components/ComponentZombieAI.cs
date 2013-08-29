//
// ComponentZombieAI.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace IhnLib {
	[Serializable]
	public class ComponentZombieAI : Component{
		public bool ShouldRefreshPath;
		public List<Vector2>Path;
		public int ChaseX;
		public int ChaseY;
		public float Speed;
		public int TileSize;
		public int PathStep;
		public int MaxSwipeTime;
		public int SwipeTime;
		public ComponentZombieAI(float speed, int tileSize, int swipeTime) {
			Speed = speed;
			TileSize = tileSize;
			Path = new List<Vector2>();
			MaxSwipeTime = swipeTime;
		}
	}
}

