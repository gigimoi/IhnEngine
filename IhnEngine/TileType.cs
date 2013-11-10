using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace IhnLib {
    /// <summary>
    /// Currently unused
    /// </summary>
    /// <param name="entity">xxx</param>
    /// <returns>xxx</returns>
	public delegate Entity TileSpawner(Entity entity);
    /// <summary>
    /// Tiles are used with System/Component Tilemap to efficiently render a tileset with minimal monotonous art effort
    /// </summary>
	[Serializable]
	public struct TileType {
        //RootTexture is the basic texture that all flairs are added to
        //CutIn is the depth that the texture is cut into if there is no tile on a side
        /// <summary>
        /// Creates a new tiletype in code, not recommended, use the ini version
        /// </summary>
        /// <param name="rootTexture"></param>
        /// <param name="cutIn"></param>
        /// <param name="flairs">List of flairs to render with the tile</param>
        /// <param name="solid">Should the tile collide</param>
        /// <param name="layer">Layer is unused, in the future there will be multiple tile layers that do not override eachother</param>
		public TileType(string rootTexture, int cutIn, List<Flair> flairs, bool solid, int layer) {
			RootTexture = rootTexture;
			CutIn = cutIn;
			Flairs = flairs;
			Solid = solid;
			Layer = layer;
			TSpawner = null;
		}
        /// <summary>
        /// Loads an ini file from a path
        /// </summary>
        /// <param name="path"></param>
		public TileType(string path) {
			var file = new IniFile(path);
			RootTexture = file.IniReadValue("Root", "Image");
			CutIn = Int32.Parse(file.IniReadValue("Root", "Cutin"));
			Solid = Boolean.Parse(file.IniReadValue("Root", "Solid"));
			TSpawner = null;
			Layer = Int32.Parse(file.IniReadValue("Root", "Layer"));
			Flairs = new List<Flair>();
			int flairCount = Int32.Parse(file.IniReadValue("Root", "Flairs"));
			for(int i = 1; i <= flairCount; i++) {
				Flairs.Add(new Flair(
					file.IniReadValue("Flair" + i, "Image"),
					Int32.Parse(file.IniReadValue("Flair" + i, "Frequency")),
					Boolean.Parse(file.IniReadValue("Flair" + i, "OnNorth")),
					Boolean.Parse(file.IniReadValue("Flair" + i, "OnEast")),
					Boolean.Parse(file.IniReadValue("Flair" + i, "OnSouth")),
					Boolean.Parse(file.IniReadValue("Flair" + i, "OnWest")),
					Int32.Parse(file.IniReadValue("Flair" + i, "MinDepth")),
					Int32.Parse(file.IniReadValue("Flair" + i, "MaxDepth"))));
			}
		}
        /// <summary>
        /// Texture that is rendered first and always
        /// </summary>
		public string RootTexture;
        /// <summary>
        /// Depth to cut into the texture when it has no solid on the side of it
        /// </summary>
		public int CutIn;
        /// <summary>
        /// List of flairs to render
        /// </summary>
		public List<Flair> Flairs;
        /// <summary>
        /// Does this tile cause collisions?
        /// </summary>
		public bool Solid;
        /// <summary>
        /// Layer this tile occupies
        /// </summary>
		public int Layer;
        /// <summary>
        /// Unused
        /// </summary>
		public TileSpawner TSpawner;
        /// <summary>
        /// Determins if this tiletype is equal to an object
        /// </summary>
        /// <param name="obj">Object to compare to</param>
        /// <returns>If this object is equal to obj</returns>
		public override bool Equals(object obj) {
			return base.Equals(obj);
		}
        /// <summary>
        /// Returns hash code for this instance
        /// </summary>
        /// <returns>Hash code</returns>
		public override int GetHashCode() {
			return base.GetHashCode();
		}
        /// <summary>
        /// Checks equality
        /// </summary>
        /// <param name="t1">TileType 1 to check</param>
        /// <param name="t2">TileType 2 to check</param>
        /// <returns>Whether the tiles are equal or not</returns>
		public static bool operator ==(TileType t1, TileType t2) {
			return t1.RootTexture == t2.RootTexture &&
				t1.CutIn == t2.CutIn &&
				t1.Flairs == t2.Flairs &&
				t1.Solid == t2.Solid &&
				t1.Layer == t2.Layer &&
				t1.TSpawner == t2.TSpawner;
		}
        /// <summary>
        /// Checks inequality
        /// </summary>
        /// <param name="t1">TileType 1 to check</param>
        /// <param name="t2">TileType 2 to check</param>
        /// <returns>Whether the tiles are inequal or not</returns>
		public static bool operator !=(TileType t1, TileType t2) {
			return !(t1 == t2);
		}
	}
    /// <summary>
    /// Drawn with TileTypes
    /// </summary>
	[Serializable]
	public struct Flair {
        /// <summary>
        /// Creates a new flair type
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="coverage"></param>
        /// <param name="north"></param>
        /// <param name="east"></param>
        /// <param name="south"></param>
        /// <param name="west"></param>
        /// <param name="minDepth"></param>
        /// <param name="maxDepth"></param>
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
        /// <summary>
        /// Texture to use for rendering this sprite
        /// </summary>
		public string Texture;
        /// <summary>
        /// %Based coverage.
        /// 100 on a 32 width sprite will create 32 flairs.
        /// Can be higher than 100.
        /// </summary>
		public int Coverage;
        /// <summary>
        /// Should this render when north is open
        /// </summary>
		public bool North;
        /// <summary>
        /// Should this render when east is open
        /// </summary>
		public bool East;
        /// <summary>
        /// Should this render when south is open
        /// </summary>
		public bool South;
        /// <summary>
        /// Should this render when west is open
        /// </summary>
		public bool West;
        /// <summary>
        /// How far into the sprite to go (Minimum)
        /// </summary>
		public int MinDepth;
        /// <summary>
        /// How far into the sprite to go (Maximum)
        /// </summary>
		public int MaxDepth;
	}
}

