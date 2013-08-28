//
// ComponentDirection.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;

namespace IhnLib {
	public class ComponentDirection : Component{
		public Direction Dir;
		public ComponentDirection(Direction dir) {
			Dir = dir;
		}
	}
}

