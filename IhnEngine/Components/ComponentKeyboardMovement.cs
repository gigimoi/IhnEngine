//
// ComponentKeyboardMovement.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;

namespace IhnLib {
	public class ComponentKeyboardMovement : Component{
		public float Speed;
		public float VerticalModifier = 1f;
		public float HorizantalModifier = 1f;
		public ComponentKeyboardMovement(float speed) {
			Speed = speed;
		}
	}
}

