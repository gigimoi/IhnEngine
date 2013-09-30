using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace IhnLib {
	[Serializable]
	public class SystemTilemap : ISystem {
		public void Update(Ihn ihn, Entity entity) {
			var tm = entity.GetComp<ComponentTilemap>();
			if(KeyHelper.KeyPressed(tm.CloseKey)) {
				tm.EditMode = !tm.EditMode;
			}
			if(MouseHelper.MouseLeftDown() && tm.EditMode) {
				var tex = Rsc.Load<Texture2D>(tm.SelectedTile.RootTexture);
				tm.PlaceTile(tm.SelectedTile, 
					Math.Max(0, (int)((MouseHelper.X + Ihn.Instance.CameraPos.X) / tex.Width)),
					Math.Max(0, (int)((MouseHelper.Y + Ihn.Instance.CameraPos.Y) / tex.Height)));
			}
		}
		Random _r = new Random();
		public void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity) {
			var tm = entity.GetComp<ComponentTilemap>();
			for(int i = (int)Math.Min(tm.Map.GetLength(0), Math.Max(0, ihn.CameraPos.X / 32));
				i < (int)Math.Min(tm.Map.GetLength(0), Math.Max(0, (ihn.CameraPos.X + 1000) / 32));
				i++) {
				for(int j = (int)Math.Min(tm.Map.GetLength(1), Math.Max(0, ihn.CameraPos.Y / 32));
					j < (int)Math.Min(tm.Map.GetLength(1), Math.Max(0, (ihn.CameraPos.Y + 1000) / 32));
					j++) {
					var tile = tm.Map[i, j];
					var seed = tm.Seeds[i, j];
					var solid = tm.MapSolids[i, j];
					var tex = tm.Textures[i, j];
					if(tile.RootTexture != "" && tile.RootTexture != null) {
						_r = new Random(seed);
						if(tex == null || tm.ForceTextureBuilds.Contains(new Vector2(i, j))) {
							_r = new Random(seed + (int)(DateTime.UtcNow.Ticks*seed));
							while(tm.ForceTextureBuilds.Contains(new Vector2(i, j))) {
								tm.ForceTextureBuilds.Remove(new Vector2(i, j));
							}
							RebuildTileTexture(tm, i, j, ref tile, solid, ref tex);
						}
						var drawRoot = new Vector2(i * tex.Width - ihn.CameraPos.X, j * tex.Height - ihn.CameraPos.Y);
						spriteBatch.Draw(tex, drawRoot, Color.White);
						for(int k = 0; k < tile.Flairs.Count; k++) {
							var flair = tile.Flairs[k];
							if(!solid.Contains(Direction.North) && flair.North) {
								for(int l = 0; l < (int)((float)flair.Coverage / 100f * ((float)tex.Width)); l++) {
									var flairtex = Rsc.Load<Texture2D>(flair.Texture);
									spriteBatch.Draw(flairtex,
										new Vector2(_r.Next(-flairtex.Width / 2, tex.Width + 2 - flairtex.Width / 2 - 1),
											-_r.Next((int)flairtex.Height / 2, flairtex.Height) +
												tm.Map[i, j].CutIn +
												tex.Height * _r.Next(flair.MinDepth,
													flair.MaxDepth) / 100f) +
											drawRoot,
										Color.White);
								}
							}
							if(!solid.Contains(Direction.South) && flair.South) {
								for(int l = 0; l < (int)((float)flair.Coverage / 100f * ((float)tex.Width)); l++) {
									var flairtex = Rsc.Load<Texture2D>(flair.Texture);
									spriteBatch.Draw(flairtex,
										new Vector2(_r.Next(-flairtex.Width / 2, tex.Width + 2 - flairtex.Width / 2 - 1),
											tex.Height + 
											-flairtex.Height + 
											_r.Next((int)flairtex.Height / 2, flairtex.Height) -
												tm.Map[i, j].CutIn -
												tex.Height * _r.Next(flair.MinDepth,
													flair.MaxDepth) / 100f) +
											drawRoot,
										Color.White);
								}
							}
						}
					}
				}
			}
		}

		private void RebuildTileTexture(ComponentTilemap tm, int i, int j, ref TileType tile, List<Direction> solid, ref Texture2D tex) {
			tex = Rsc.Load<Texture2D>(tile.RootTexture);
			if(!solid.Contains(Direction.North)) {
				tex = tex.FillRectangle(new Rectangle(0, 0, tex.Width, tile.CutIn), Color.Transparent);
				var rects = new List<Rectangle>();
				for(int k = 0; k < _r.Next((int)(tex.Width / 2f), (int)(tex.Width / 1.2f)); k++) {
					rects.Add(new Rectangle(_r.Next(0, tex.Width - 1), tile.CutIn, _r.Next(1, 3), _r.Next(1, 3)));
				}
				tex = tex.FillRectangles(rects, Color.Transparent);
			}
			if(!solid.Contains(Direction.South)) {
				tex = tex.FillRectangle(new Rectangle(0, tex.Height - tile.CutIn, tex.Width, tile.CutIn), Color.Transparent);
				var rects = new List<Rectangle>();
				for(int k = 0; k < _r.Next((int)(tex.Width / 2f), (int)(tex.Width / 1.2f)); k++) {
					rects.Add(new Rectangle(_r.Next(0, tex.Width - 1), tex.Height - tile.CutIn, _r.Next(1, 3), _r.Next(1, 3)));
				}
				tex = tex.FillRectangles(rects, Color.Transparent);
			}
			Console.WriteLine("Built texture" + DateTime.Now.Millisecond);
			tm.Textures[i, j] = tex;
		}
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {
					typeof(ComponentTilemap)
				};
			}
		}
	}
}

