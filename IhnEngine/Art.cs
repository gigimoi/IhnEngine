//
// Art.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IhnLib {
	public static class Art {
		static Texture2D _pixel;
		public static Texture2D GetPixel() {
			if(_pixel == null) {
				_pixel = new Texture2D(Ihn.Instance.GraphicsDevice, 1, 1);
				_pixel.SetData<Color>(new Color[] { Color.White });
			}
			return _pixel;
		}
	}
}
