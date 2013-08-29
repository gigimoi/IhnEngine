//
// ComponentVelocity.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;

namespace IhnLib {
	[Serializable]
	public class ComponentVelocity : Component{
		public ComponentVelocity() : this(0, 0) { }
		public ComponentVelocity(float x, float y) {
			X = x;
			Y = y;
		}
		public float X;
		public float Y;
	}
}

