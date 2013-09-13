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
		static Texture2D _none;
		public static Texture2D GetPixel() {
			if(_pixel == null) {
				_pixel = new Texture2D(Ihn.Instance.GraphicsDevice, 1, 1);
				_pixel.SetData<Color>(new Color[] { Color.White });
			}
			return _pixel;
		}
		public static Texture2D GetNone() {
			if(_none == null) {
				_none = new Texture2D(Ihn.Instance.GraphicsDevice, 1, 1);
				_none.SetData<Color>(new Color[] { new Color(0, 0, 0, 0) });
			}
			return _none;
		}
	}
}

