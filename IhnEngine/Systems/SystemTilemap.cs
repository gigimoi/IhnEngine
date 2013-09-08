//
// SystemTilemap.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace IhnLib {
	[Serializable]
	public class SystemTilemap : ISystem{
		public Texture2D Outline;
		public Vector2 OutlinePos;
		public Vector2 TilePos;
		public Vector2 LastPos = new Vector2(-1, -1);
		public bool Dragging;
		public bool[,] SolidityMap = new bool[5, 5];

		public List<Type> ComponentsPathingWhitelist = new List<Type> {
		};
		
		public SystemTilemap() {
			EventManager.Listen("Tilemap Changed", updateSolidity);
		}
		void updateSolidity(object sender, EventArgs e) {
			var entities = Ihn.Instance.GetEntitiesWith<ComponentTilesetSprite>();
			float highx = 0;
			float highy = 0;
			for(int i = 0; i < entities.Count; i++) {
				var pos = entities[i].GetComp<ComponentPosition>();
				var sprite = entities[i].GetComp<ComponentTilesetSprite>();
				highx = Math.Max(pos.X / sprite.TType.Size, highx);
				highy = Math.Max(pos.Y / sprite.TType.Size, highy);
			}
			if(highx > SolidityMap.GetLength(0) - 1 ||
			   highy > SolidityMap.GetLength(1) - 1) {
				SolidityMap = new bool[(int)highx + 1, (int)highy + 1];
			}
			for(int i = 0; i < entities.Count; i++) {
				var pos = entities[i].GetComp<ComponentPosition>();
				var sprite = entities[i].GetComp<ComponentTilesetSprite>();
				var passByWhitelist = false;
				if(ComponentsPathingWhitelist.Count > 0) {
					for(int j = 0; j < entities[i].Components.Count; j++) {
						if(ComponentsPathingWhitelist.Contains(entities[i].GetComp(j).GetType())) {
							passByWhitelist = true;
							break;
						}
					}
				}
				if(!passByWhitelist && entities[i].HasComp<ComponentSolid>()) {
					SolidityMap[(int)pos.X / sprite.TType.Size, (int)pos.Y / sprite.TType.Size] = true;
				}
				else {
					SolidityMap[(int)pos.X / sprite.TType.Size, (int)pos.Y / sprite.TType.Size] = false;
				}
			}
		}
		public void Update(Ihn ihn, Entity entity) {
			var teditor = entity.GetComp<ComponentTilemap>();
			int size = teditor.Tiles[teditor.Selected].Size;
			int mx = MouseHelper.X / size * size;
			int my = MouseHelper.Y / size * size;
			if(MouseHelper.MouseLeftDown() && !Dragging && KeyHelper.KeyDown(Keys.LeftShift)) {
				LastPos = new Vector2(mx, my);
				Dragging = true;
			}
			if(KeyHelper.KeyReleased(Keys.LeftShift)) {
				Dragging = false;
			}
			if(Dragging && MouseHelper.MouseLeftReleased() && KeyHelper.KeyDown(Keys.LeftShift)) {
				if(LastPos.X > mx) {
					int i = (int)LastPos.X;
					LastPos.X = mx;
					mx = i;
				}
				if(LastPos.Y > my) {
					int i = (int)LastPos.Y;
					LastPos.Y = my;
					my = i;
				}
				for(int i = (int)LastPos.X; i <= mx; i += size) {
					for(int j = (int)LastPos.Y; j <= my; j += size) {
						PlaceTile(ihn, teditor.Tiles[teditor.Selected], i, j);
					}
				}
				mx = MouseHelper.X / size * size;
				my = MouseHelper.Y / size * size;
				LastPos = new Vector2(-1, -1);
				Dragging = false;
			}
			if(MouseHelper.MouseLeftDown() && !Dragging) {
				PlaceTile(ihn, teditor.Tiles[teditor.Selected], mx, my);
			}
			if(MouseHelper.WheelDelta > 0) {
				teditor.Selected = Math.Min(teditor.Tiles.Count - 1, teditor.Selected + 1);
			}
			if(MouseHelper.WheelDelta < 0) {
				teditor.Selected = Math.Max(0, teditor.Selected - 1);
			}
		}

		public void PlaceTile(Ihn ihn, TileType tileType, int x, int y) {
			var tiles = ihn.GetEntitiesWith<ComponentTilesetSprite>();
			for(int i = 0; i < tiles.Count; i++) {
				var tileSprite = tiles[i].GetComp<ComponentTilesetSprite>();
				var tilePos = tiles[i].GetComp<ComponentPosition>();
				if(tilePos.X == x && tilePos.Y == y && tileSprite.TType.Layer == tileType.Layer) {
					ihn.RemoveEntity(tiles[i]);
				}
			}
			Entity newTile = new Entity();
			newTile.AddComp(new ComponentPosition(x, y));
			if(tileType.Solid) {
				newTile.AddComp(new ComponentSolid());
			}
			newTile.AddComp(new ComponentTilesetSprite(tileType));
			if(tileType.TSpawner != null) {
				newTile = tileType.TSpawner.Invoke(newTile);
				EventManager.Raise("Tilemap Changed");
			}
			else {
				if(SolidityMap.GetLength(0) < x / tileType.Size + 1 || SolidityMap.GetLength(1) < y / tileType.Size + 1) {
					EventManager.Raise("Tilemap Changed");
				}
				else {
					SolidityMap[(int)x / tileType.Size, (int)y / tileType.Size] = tileType.Solid;
				}
			}
			ihn.AddEntity(newTile);
		}

		public void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity) {
			var teditor = entity.GetComp<ComponentTilemap>();
			if(Outline != null) {
				spriteBatch.Draw(Rsc.Load<Texture2D>(teditor.Tiles[teditor.Selected].Texture), 
				                 new Rectangle((int)TilePos.X, (int)TilePos.Y, teditor.Tiles[teditor.Selected].Size, teditor.Tiles[teditor.Selected].Size), 
				                 new Rectangle(0, 0, teditor.Tiles[teditor.Selected].Size, teditor.Tiles[teditor.Selected].Size), 
				                 Color.White);
				spriteBatch.Draw(Outline, OutlinePos, Color.White);
			}
			if(Dragging) {
				int size = teditor.Tiles[teditor.Selected].Size;
				int mx = MouseHelper.X / size * size;
				int my = MouseHelper.Y / size * size;
				int lpx = (int)LastPos.X;
				int lpy = (int)LastPos.Y;
				if(LastPos.X > mx) {
					lpx = mx;
					mx = (int)LastPos.X;
				}
				if(LastPos.Y > my) {
					lpy = my;
					my = (int)LastPos.Y;
				}
				for(int i = lpx; i <= mx + size; i++) {
					spriteBatch.Draw(Art.GetPixel(), new Vector2(i, my + size), Color.White);
					spriteBatch.Draw(Art.GetPixel(), new Vector2(i, lpy), Color.White);
				}
				for(int i = lpy; i <= my + size; i++) {
					spriteBatch.Draw(Art.GetPixel(), new Vector2(mx + size, i), Color.White);
					spriteBatch.Draw(Art.GetPixel(), new Vector2(lpx, i), Color.White);
				}
			}
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

