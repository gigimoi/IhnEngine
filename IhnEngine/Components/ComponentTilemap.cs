using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
    /// <summary>
    /// Stores data for a tilemap
    /// </summary>
	[Serializable]
	public class ComponentTilemap : Component {
        /// <summary>
        /// Key used to enable/Disable editting
        /// </summary>
		public Keys EditModeToggleKey = Keys.F3;
        /// <summary>
        /// Allow EditModeToggleKey to function
        /// </summary>
        public bool AllowEditModeToggle = false;
        /// <summary>
        /// Currently in edit mode?
        /// </summary>
		public bool EditMode = false;
        /// <summary>
        /// Currently selected edit mode tile
        /// </summary>
		public TileType SelectedTile;
        /// <summary>
        /// 2D array of tile types
        /// </summary>
		public TileType[,] Map = new TileType[100, 100];
        /// <summary>
        /// 2D array of lists that the tiles collide into
        /// </summary>
		public List<Direction>[,] MapSolids = new List<Direction>[100, 100];
        /// <summary>
        /// 2D array of seeds used to generate flairs
        /// </summary>
		public int[,] Seeds = new int[100, 100];
        /// <summary>
        /// List of vectors to force texture rebuilds at
        /// </summary>
		[NonSerialized]
		public List<Vector2> ForceTextureBuilds = new List<Vector2>();
        /// <summary>
        /// Nonserialized 2D array of textures, regenerated when loaded
        /// </summary>
		[NonSerialized]
		public Texture2D[,] Textures = new Texture2D[100, 100];
		Random _r = new Random();
        /// <summary>
        /// Instantiates a new default tilemap
        /// </summary>
		public ComponentTilemap() {
			for(int i = 0; i < MapSolids.GetLength(0); i++) { for(int j = 0; j < MapSolids.GetLength(1); j++) { MapSolids[i, j] = new List<Direction>(); Seeds[i, j] = (int)_r.Next() * 1000; } }
		}
        /// <summary>
        /// Places a tile
        /// </summary>
        /// <param name="tileType">Type of tile to place</param>
        /// <param name="x">XCoord of tile placement</param>
        /// <param name="y">YCoord of tile placement</param>
        /// <param name="updateTiles">Should it update tiles near it</param>
		public void PlaceTile(TileType tileType, int x, int y, bool updateTiles = true) {
			Map[x, y] = tileType;
			if(updateTiles) {
				for(int i = Math.Min(Map.GetLength(0), Math.Max(0, x - 1)); i <= Math.Min(x + 1, Map.GetLength(0)); i++) {
					for(int j = Math.Min(Map.GetLength(1), Math.Max(0, y - 1)); j <= Math.Min(y + 1, Map.GetLength(1)); j++) {
						updateSolids(i, j);
						ForceTextureBuilds.Add(new Vector2(i, j));
					}
				}
			}
		}
		private void updateSolids(int x, int y) {
            if (x >= 0 && y >= 0 && x < MapSolids.GetLength(0) && y < MapSolids.GetLength(1)) {
                MapSolids[x, y].Clear();
                if (y - 1 >= 0) {
                    if (Map[x, y - 1].Solid == Map[x, y].Solid) { MapSolids[x, y].Add(Direction.North); }
                }
                if (x + 1 < MapSolids.GetLength(0)) {
                    if (Map[x + 1, y].Solid == Map[x, y].Solid) { MapSolids[x, y].Add(Direction.East); }
                }
                if (y + 1 < MapSolids.GetLength(1)) {
                    if (Map[x, y + 1].Solid == Map[x, y].Solid) { MapSolids[x, y].Add(Direction.South); }
                }
                if (x - 1 >= 0) {
                    if (Map[x - 1, y].Solid == Map[x, y].Solid) { MapSolids[x, y].Add(Direction.West); }
                }
            }
		}
	}
}
