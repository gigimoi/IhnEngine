using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
	[Serializable]
	public class ComponentTilemap : Component {
		public TileType SelectedTile;
		public TileType[,] Map = new TileType[500, 200];
		public List<Direction>[,] MapSolids = new List<Direction>[500, 200];
		public int[,] Seeds = new int[500, 200];
		public List<Vector2> ForceTextureBuilds = new List<Vector2>();
		[NonSerialized]
		public Texture2D[,] Textures = new Texture2D[500, 200];
		public ComponentTilemap() {
			for(int i = 0; i < MapSolids.GetLength(0); i++) { for(int j = 0; j < MapSolids.GetLength(1); j++) { MapSolids[i, j] = new List<Direction>(); Seeds[i, j] = (int)DateTime.Now.Ticks; } }
		}
		public void PlaceTile(TileType tileType, int x, int y, bool updateTiles = true) {
			Map[x, y] = tileType;
			if(updateTiles) {
				for(int i = Math.Max(0, x - 1); i <= Math.Min(x + 1, Map.GetLength(0)); i++) {
					for(int j = Math.Max(0, y - 1); j <= Math.Min(y + 1, Map.GetLength(1)); j++) {
						updateSolids(i, j);
						ForceTextureBuilds.Add(new Vector2(i, j));
					}
				}
			}
		}
		private void updateSolids(int x, int y) {
			MapSolids[x, y].Clear();
			if(y - 1 >= 0) {
				if(Map[x, y - 1] == Map[x, y]) { MapSolids[x, y].Add(Direction.North); }
			}
			if(x + 1 < MapSolids.GetLength(0)) {
				if(Map[x + 1, y] == Map[x, y]) { MapSolids[x, y].Add(Direction.East); }
			}
			if(y + 1 < MapSolids.GetLength(1)) {
				if(Map[x, y + 1] == Map[x, y]) { MapSolids[x, y].Add(Direction.South); }
			}
			if(x - 1 >= 0) {
				if(Map[x - 1, y] == Map[x, y]) { MapSolids[x, y].Add(Direction.West); }
			}
		}
	}
}
