//
// ComponentPosition.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;

namespace IhnLib {
	[Serializable]
	public class ComponentPosition : Component{
		public ComponentPosition(float x, float y) {
			X = x;
			Y = y;
		}
		public float X;
		public float Y;
	}
}

