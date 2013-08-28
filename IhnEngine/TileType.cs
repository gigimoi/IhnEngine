//
// TileType.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace IhnLib {
	public delegate Entity TileSpawner(Entity entity);
	public struct TileType {
		int _size;
		public int Size {
			get {
				if(_size == 0) {
					_size = AutoTiled ? Texture.Width / 7 : Texture.Width;
				}
				return _size;
			}
		}
		public Texture2D Texture;
		public bool AutoTiled;
		public bool Solid;
		public int Layer;
		public TileSpawner TSpawner;
	}
}

