using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace IhnLib {
    /// <summary>
    /// Renders and allows editing of a tilemap
    /// </summary>
	[Serializable]
	public class SystemTilemap : ISystem {
        /// <summary>
        /// Enable to cause tiles to spazm when placed. Much fun
        /// </summary>
        public static bool Spaz = false;
        /// <summary>
        /// Updates the tilemap
        /// </summary>
        /// <param name="ihn">Ihn entity is contained in</param>
        /// <param name="entity">Entity to update</param>
		public void Update(Ihn ihn, Entity entity) {
			var tm = entity.GetComp<ComponentTilemap>();
            if (KeyHelper.KeyPressed(tm.EditModeToggleKey) && tm.AllowEditModeToggle) {
				tm.EditMode = !tm.EditMode;
			}
			if(MouseHelper.MouseLeftDown() && tm.EditMode && tm.SelectedTile != default(TileType)) {
				var tex = Rsc.Load<Texture2D>(tm.SelectedTile.RootTexture);
				tm.PlaceTile(tm.SelectedTile, 
					Math.Max(0, (int)((MouseHelper.X + Ihn.Instance.CameraPos.X) / tex.Width)),
					Math.Max(0, (int)((MouseHelper.Y + Ihn.Instance.CameraPos.Y) / tex.Height)));
			}
			if(MouseHelper.MouseRightDown() && tm.EditMode) {
				var tex = Rsc.Load<Texture2D>(tm.SelectedTile.RootTexture);
				tm.PlaceTile(new TileType(),
					Math.Max(0, (int)((MouseHelper.X + Ihn.Instance.CameraPos.X) / tex.Width)),
					Math.Max(0, (int)((MouseHelper.Y + Ihn.Instance.CameraPos.Y) / tex.Height)));
			}
		}
		Random _r = new Random();
        /// <summary>
        /// Renders the tilemap
        /// </summary>
        /// <param name="ihn">Ihn entity is contained in</param>
        /// <param name="spriteBatch">Spritebatch to draw with</param>
        /// <param name="entity">Entity to draw</param>
		public void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity) {
			var tm = entity.GetComp<ComponentTilemap>();
			for(int i = (int)Math.Min(tm.Map.GetLength(0), Math.Max(0, ihn.CameraPos.X / 32));
				    i < (int)Math.Min(tm.Map.GetLength(0), Math.Max(0, (ihn.CameraPos.X + 1000) / 32));
				    i++) {
                for (int j = (int)Math.Min(tm.Map.GetLength(1), Math.Max(0, ihn.CameraPos.Y / 32));
                        j < (int)Math.Min(tm.Map.GetLength(1), Math.Max(0, (ihn.CameraPos.Y + 1000) / 32));
                        j++) {
                    PreRenderTile(ihn, spriteBatch, tm, i, j);
                }
			}
		}
        /// <summary>
        /// Checks if a tile needs to be recut and cuts it
        /// </summary>
        /// <param name="ihn"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tm"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        private void PreRenderTile(Ihn ihn, SpriteBatch spriteBatch, ComponentTilemap tm, int i, int j, int rx = -1, int ry = -1) {
            var tile = tm.Map[i, j];
            var seed = tm.Seeds[i, j];
            var solid = tm.MapSolids[i, j];
            if (tm.Textures == null) {
                tm.Textures = new Texture2D[tm.Map.GetLength(0), tm.Map.GetLength(1)];
            }
            var tex = tm.Textures[i, j];
            if (tile.RootTexture != "" && tile.RootTexture != null) {
                _r = new Random(seed);
                if (tex == null || tm.ForceTextureBuilds.Contains(new Vector2(i, j))) {
                    _r = new Random(seed + (Spaz ?  (int)(DateTime.UtcNow.Ticks * seed) : 0));
                    if(tm.ForceTextureBuilds == null) {
						tm.ForceTextureBuilds = new List<Vector2>();
						for(int x = 0; x < tm.Map.GetLength(0); x++) {
							for(int y = 0; y < tm.Map.GetLength(1); y++) {
								tm.ForceTextureBuilds.Add(new Vector2(x, y));
							}
						}
					}
					while (tm.ForceTextureBuilds.Contains(new Vector2(i, j))) {
                        tm.ForceTextureBuilds.Remove(new Vector2(i, j));
                    }
                    RebuildTileTexture(tm, i, j, ref tile, solid, ref tex);
                }
                var drawRoot = new Vector2(i * tex.Width - ihn.CameraPos.X, j * tex.Height - ihn.CameraPos.Y);
                if (rx != -1 || ry != -1) {
                    drawRoot.X = rx;
                    drawRoot.Y = ry;
                }
                RenderTile(spriteBatch, tm, tile, solid, drawRoot, _r, tex);
            }
        }
        //TODO: Render tile to a rendertarget, save, and render that instead of generating flairs at runtime
        /// <summary>
        /// Renders a tile at drawRoot
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to render with</param>
        /// <param name="tm">Tilemap to render from</param>
        /// <param name="tile">TileType to render</param>
        /// <param name="solid">List of directions that the tile considers solid</param>
        /// <param name="drawRoot">Literal x and y to draw at</param>
        /// <param name="_r">Random to generate with</param>
        /// <param name="tex">Prebuilt texture, will build if null</param>
        public static void RenderTile(SpriteBatch spriteBatch, ComponentTilemap tm,  TileType tile, List<Direction> solid, Vector2 drawRoot, Random _r = null, Texture2D tex = null) {
            tex = tex != null ? tex : Rsc.Load<Texture2D>(tile.RootTexture);
            spriteBatch.Draw(tex, new Rectangle((int)drawRoot.X, (int)drawRoot.Y, tex.Width, tex.Height),
                             null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
            _r = _r != null ? _r : new Random(100);
            for (int k = 0; k < tile.Flairs.Count; k++) {
                var flair = tile.Flairs[k];
                if (!solid.Contains(Direction.North) && flair.North) {
                    for (int l = 0; l < (int)((float)flair.Coverage / 100f * ((float)tex.Width)); l++) {
                        var flairtex = Rsc.Load<Texture2D>(flair.Texture);
                        spriteBatch.Draw(flairtex,
                            new Rectangle(
                                (int)(drawRoot.X +
                                    _r.Next(-flairtex.Width / 2, tex.Width + 2 - flairtex.Width / 2 - 1)),
                                (int)(drawRoot.Y + -_r.Next((int)flairtex.Height / 2, flairtex.Height) +
                                    tile.CutIn +
                                    tex.Height * _r.Next(flair.MinDepth,
                                        flair.MaxDepth) / 100f),
                                flairtex.Width,
                                flairtex.Height),
                            null,
                            Color.White,
                            0, new Vector2(0, 0),
                            SpriteEffects.None, 0);
                    }
                }
                if (!solid.Contains(Direction.South) && flair.South) {
                    for (int l = 0; l < (int)((float)flair.Coverage / 100f * ((float)tex.Width)); l++) {
                        var flairtex = Rsc.Load<Texture2D>(flair.Texture);
                        spriteBatch.Draw(flairtex,
                            new Rectangle(
                                (int)(drawRoot.X +
                                    _r.Next(-flairtex.Width / 2, tex.Width + 2 - flairtex.Width / 2 - 1)),
                                (int)(drawRoot.Y +
                                    tex.Height +
                                    -flairtex.Height +
                                    _r.Next((int)flairtex.Height / 2, flairtex.Height) -
                                    tile.CutIn -
                                    tex.Height * _r.Next(flair.MinDepth,
                                        flair.MaxDepth) / 100f),
                                flairtex.Width,
                                flairtex.Height),
                            null,
                            Color.White,
                            0, new Vector2(0, 0),
                            SpriteEffects.None, 0);
                    }
                }
            }
        }
        //Cuts into a rectangle
		private void RebuildTileTexture(ComponentTilemap tm, int i, int j, ref TileType tile, List<Direction> solid, ref Texture2D tex) {
			tex = Rsc.Load<Texture2D>(tile.RootTexture);
			//TODO: GetData broken on linux/mac, implement
			/*
			if(!solid.Contains(Direction.North)) {
				tex = tex.FillRectangle(new Rectangle(0, 0, tex.Width, tile.CutIn), Color.Transparent);
				var rects = new List<Rectangle>();
				for(int k = 0; k < _r.Next((int)(tex.Width / 2f), (int)(tex.Width / 1.2f)); k++) {
					rects.Add(new Rectangle(_r.Next(0, tex.Width - 1), tile.CutIn, _r.Next(1, 3), _r.Next(1, 3)));
				}
				tex = tex.FillRectangles(rects, Color.Transparent);
			}
			if(!solid.Contains(Direction.South)) {
				var rects = new List<Rectangle>();
				for(int k = 0; k < _r.Next((int)(tex.Width / 2f), (int)(tex.Width / 1.2f)); k++) {
					rects.Add(new Rectangle(_r.Next(0, tex.Width - 1), tex.Height - tile.CutIn + 1, _r.Next(1, 3), _r.Next(1, 3)));
				}
				tex = tex.FillRectangles(rects, Color.Transparent);
			}
			*/			
			tm.Textures[i, j] = tex;
		}
        /// <summary>
        /// List of Components required to run on an entity, ComponentTileMap
        /// </summary>
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {
					typeof(ComponentTilemap)
				};
			}
		}
	}
}

