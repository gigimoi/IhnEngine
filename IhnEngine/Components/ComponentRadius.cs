//
// ComponentRadius.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;

namespace IhnLib {
	[Serializable]
	public class ComponentRadius : Component{
		public int Radius;
		public ComponentRadius(int radius) {
			Radius = radius;
		}
	}
}

