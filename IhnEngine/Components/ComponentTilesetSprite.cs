//
// ComponentTilesetSprite.cs
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
	public class ComponentTilesetSprite : ComponentSprite{
		public TileType TType;

		public bool North;
		public bool NorthEast;
		public bool East;
		public bool SouthEast;
		public bool South;
		public bool SouthWest;
		public bool West;
		public bool NorthWest;

		public bool Recalced;
		public bool ForceRecalc;

		public ComponentTilesetSprite(TileType ttype) : base(ttype.Texture, new FloatRect(0, 0, ttype.Size, ttype.Size), new Vector2(0, 0)) {
			TType = ttype;
			ForceRecalc = true;
		}
	}
}

