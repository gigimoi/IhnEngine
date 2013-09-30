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
		public TileType(string rootTexture, int cutIn, List<Flair> flairs, bool solid, int layer) {
			RootTexture = rootTexture;
			CutIn = cutIn;
			Flairs = flairs;
			Solid = solid;
			Layer = layer;
			TSpawner = null;
		}
		public string RootTexture;
		public int CutIn;
		public List<Flair> Flairs;
		public bool Solid;
		public int Layer;
		public TileSpawner TSpawner;
		public override bool Equals(object obj) {
			return base.Equals(obj);
		}
		public override int GetHashCode() {
			return base.GetHashCode();
		}
		public static bool operator ==(TileType t1, TileType t2) {
			return t1.RootTexture == t2.RootTexture &&
				t1.CutIn == t2.CutIn &&
				t1.Flairs == t2.Flairs &&
				t1.Solid == t2.Solid &&
				t1.Layer == t2.Layer &&
				t1.TSpawner == t2.TSpawner;
		}
		public static bool operator !=(TileType t1, TileType t2) {
			return !(t1 == t2);
		}
	}
	[Serializable]
	public struct Flair {
		public Flair(string texture, int coverage, bool north, bool east, bool south, bool west, int minDepth, int maxDepth) {
			Texture = texture;
			Coverage = coverage;
			North = north;
			East = east;
			South = south;
			West = west;
			MinDepth = minDepth;
			MaxDepth = maxDepth;
		}
		public string Texture;
		public int Coverage;
		public bool North;
		public bool East;
		public bool South;
		public bool West;
		public int MinDepth;
		public int MaxDepth;
	}
}

