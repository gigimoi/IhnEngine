//
// ComponentGravity.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;

namespace IhnLib {
	[Serializable]
	public class ComponentGravity : Component{
		public static float GlobalMultiplier = 1f;
		public float Multiplier = 1;
		public ComponentGravity(float multiplier = 1) {
			Multiplier = multiplier;
		}
	}
}