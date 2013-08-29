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
	[Serializable]
	public struct TileType {
		int _size;
		public int Size {
			get {
				if(_size == 0) {
					_size = AutoTiled ? Rsc.Load<Texture2D>(Texture).Width / 7 : Rsc.Load<Texture2D>(Texture).Width;
				}
				return _size;
			}
		}
		public string Texture;
		public bool AutoTiled;
		public bool Solid;
		public int Layer;
		public TileSpawner TSpawner;
	}
}

