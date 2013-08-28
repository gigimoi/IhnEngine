//
// ComponentAABB.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;

namespace IhnLib {
	public class ComponentAABB : Component{
		public int Width;
		public int Height;
		public int X;
		public int Y;
		public ComponentAABB(int x, int y, int w, int h) {
			Width = w;
			Height = h;
			X = x;
			Y = y;
		}
	}
}

