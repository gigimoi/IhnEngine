//
// ComponentTilemap.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace IhnLib {
	[Serializable]
	public class ComponentTilemap : Component {
		public bool Enabled = true;
		public List<TileType> Tiles = new List<TileType>();
		public int Selected;
	}
}

