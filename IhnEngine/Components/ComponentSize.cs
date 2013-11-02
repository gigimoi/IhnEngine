//
// ComponentSize.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;

namespace IhnLib {
	[Serializable]
	public class ComponentSize : Component{
		public int Width;
		public int Height;
		public ComponentSize(int width, int height) {
			Width = width;
			Height = height;
		}
	}
}

