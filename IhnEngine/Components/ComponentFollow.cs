//
// ComponentFollow.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using Microsoft.Xna.Framework;

namespace IhnLib {
	[Serializable]
	public class ComponentFollow : Component{
		public Entity ToFollow;
		public Position Offset;

		public ComponentFollow(Entity toFollow, Position offset) {
			if(!toFollow.HasComp<ComponentPosition>()) {
				Console.WriteLine("WARNING: Trying to follow component without position");
			}
			ToFollow = toFollow;
			Offset = offset;
		}
		public ComponentFollow(Entity toFollow) : this(toFollow, new Position(0, 0)) { }
	}
}

