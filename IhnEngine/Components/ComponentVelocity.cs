//
// ComponentVelocity.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;

namespace IhnEngine {
	public class ComponentVelocity : Component{
		public ComponentVelocity() : this(0, 0) { }
		public ComponentVelocity(int x, int y) {
			X = x;
			Y = y;
		}
		public int X;
		public int Y;
	}
}

