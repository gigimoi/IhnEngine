//
// FloatRect.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using Microsoft.Xna.Framework;

namespace IhnLib {
	[Serializable]
	public class FloatRect {
		public float X;
		public float Y;
		public float Width;
		public float Height;
		public FloatRect(float x, float y, float w, float h) {
			X = x;
			Y = y;
			Width = w;
			Height = h;
		}
		public Rectangle ToRect() {
			return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
		}
	}
}
