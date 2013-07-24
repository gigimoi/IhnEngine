//
// ComponentPosition.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;

namespace IhnEngine {
	public class ComponentPosition : Component{
		public ComponentPosition(int x, int y) {
			X = x;
			Y = y;
		}
		public int X;
		public int Y;
	}
}

