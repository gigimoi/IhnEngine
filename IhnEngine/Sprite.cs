//
// Sprite.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IhnLib {
	[Serializable]
	public class Sprite {
		private Color[,] spr = new Color[1,1];
		public int Width {get {
				return spr.GetLength(0);
			}}
		public int Height {get {
				return spr.GetLength(0);
			}}
		public Texture2D XNATex {get {
				var tex = new Texture2D(Ihn.Instance.GraphicsDevice, spr.GetLength(0), spr.GetLength(1));
				for(int i = 0; i < spr.GetLength(0); i++) {
					for(int j = 0; j < spr.GetLength(1); j++) {
						var col = new Color[1] { spr[i, j] };
						tex.SetData(col, j + i * j, 1);
					}
				}
				return tex;
			}}
		public Sprite(Texture2D texture) {
			for(int i = 0; i < texture.Width; i++) {
				for(int j = 0; j < texture.Height; j++) {
					var col = new Color[1];
					texture.GetData<Color>(col, j + i * j, 1);
					//spr[i, j] = col[0];
				}
			}
		}
	}
}

