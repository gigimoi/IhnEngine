//
// SystemRenderTilesetSprite.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace IhnLib {
	public class SystemRenderTilesetSprite : ISystem{
		int recalcing = 0;
		public bool DrawBorder = false;
		public SystemRenderTilesetSprite() {
			EventManager.Listen("Tilemap Changed", CallbackRecalcAutotiling);
			EventManager.Listen("Ihn Update Start", CallbackUpdate);
		}
		void CallbackUpdate(object sender, EventArgs e) {
			recalcing--;
		}

		public void CallbackRecalcAutotiling(object sender, EventArgs e) {
			recalcing = 2;
		}
		public void Update(Ihn ihn, Entity entity) {
			var sprite = entity.GetComp<ComponentTilesetSprite>();
			if(recalcing == 0) {
				var pos = entity.GetComp<ComponentPosition>();
				var similarNeighbors = ihn.GetEntitiesWith<ComponentTilesetSprite>();
				for(int i = 0; i < similarNeighbors.Count; i++) {
					var otherPos = similarNeighbors[i].GetComp<ComponentPosition>();
					var othersprite = similarNeighbors[i].GetComp<ComponentTilesetSprite>();
					bool toSet = ( similarNeighbors[i].HasComp<ComponentSolid>() &&  entity.HasComp<ComponentSolid>()) ||
						         (!similarNeighbors[i].HasComp<ComponentSolid>() && !entity.HasComp<ComponentSolid>());
					if(otherPos.X == pos.X && otherPos.Y == pos.Y - sprite.TType.Size) {
						sprite.North = toSet;
					} else if(otherPos.X == pos.X + sprite.TType.Size && otherPos.Y == pos.Y) {
						sprite.East = toSet;
					} else if(otherPos.X == pos.X && otherPos.Y == pos.Y + sprite.TType.Size) {
						sprite.South = toSet;
					} else if(otherPos.X == pos.X - sprite.TType.Size && otherPos.Y == pos.Y) {
						sprite.West = toSet;
					} else if(otherPos.X == pos.X - sprite.TType.Size && otherPos.Y == pos.Y - sprite.TType.Size) {
						sprite.NorthWest = toSet;
					} else if(otherPos.X == pos.X - sprite.TType.Size && otherPos.Y == pos.Y + sprite.TType.Size) {
						sprite.SouthWest = toSet;
					} else if(otherPos.X == pos.X + sprite.TType.Size && otherPos.Y == pos.Y - sprite.TType.Size) {
						sprite.NorthEast = toSet;
					} else if(otherPos.X == pos.X + sprite.TType.Size && otherPos.Y == pos.Y + sprite.TType.Size) {
						sprite.SouthWest = toSet;
					}
				}
			}
		}
		public void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity) {
			var pos = entity.GetComp<ComponentPosition>();
			var sprite = entity.GetComp<ComponentTilesetSprite>();
			bool north = sprite.North;
			bool south = sprite.South;
			bool east = sprite.East;
			bool west = sprite.West;
			bool northwest = sprite.NorthWest;
			bool northeast = sprite.NorthEast;
			bool southwest = sprite.SouthWest;
			bool southeast = sprite.SouthEast;
			if(sprite.TType.AutoTiled) {
				Vector2 vector = new Vector2(2, 2);
				if(!north && !east && !south && !west) {
					vector = new Vector2(sprite.TType.Size * 0, sprite.TType.Size * 3);
				}
				if(!north && east && southeast && south && !west) {
					vector = new Vector2(sprite.TType.Size * 0, sprite.TType.Size * 1);
				}
				if(!north && !east && south && southwest && west) {
					vector = new Vector2(sprite.TType.Size * 1, sprite.TType.Size * 1);
				}
				if(!north && east && !south && !west) {
					vector = new Vector2(sprite.TType.Size * 2, sprite.TType.Size * 1);
				}
				if(!north && !east && !south && west) {
					vector = new Vector2(sprite.TType.Size * 3, sprite.TType.Size * 1);
				}
				if(north && northeast && east && !south && !west) {
					vector = new Vector2(sprite.TType.Size * 0, sprite.TType.Size * 2);
				}
				if(north && !east && !south && west && northwest) {
					vector = new Vector2(sprite.TType.Size * 1, sprite.TType.Size * 2);
				}
				if(!north && east && !south && west) {
					vector = new Vector2(sprite.TType.Size * 2, sprite.TType.Size * 2);
				}
				if(north && !east && south && !west) {
					vector = new Vector2(sprite.TType.Size * 3, sprite.TType.Size * 2);
				}
				if(north && !east && !south && !west) {
					vector = new Vector2(sprite.TType.Size * 1, sprite.TType.Size * 3);
				}
				if(!north && !east && south && !west) {
					vector = new Vector2(sprite.TType.Size * 2, sprite.TType.Size * 3);
				}
				if(!north && east && !southeast && south && !west) {
					vector = new Vector2(sprite.TType.Size * 0, sprite.TType.Size * 4);
				}
				if(!north && !east && south && !southwest && west) {
					vector = new Vector2(sprite.TType.Size * 1, sprite.TType.Size * 4);
				}
				if(north && !northeast && east && !south && !west) {
					vector = new Vector2(sprite.TType.Size * 0, sprite.TType.Size * 5);
				}
				if(north && !east && !south && west && !northwest) {
					vector = new Vector2(sprite.TType.Size * 1, sprite.TType.Size * 5);
				}
				if(north && northeast && east && !south & west && northwest) {
					vector = new Vector2(sprite.TType.Size * 0, sprite.TType.Size * 0);
				}
				if(north && northeast && east && southeast && south && !west) {
					vector = new Vector2(sprite.TType.Size * 1, sprite.TType.Size * 0);
				}
				if(!north && east && southeast && south && southwest && west) {
					vector = new Vector2(sprite.TType.Size * 2, sprite.TType.Size * 0);
				}
				if(north && !east && south && southwest && west && northwest) {
					vector = new Vector2(sprite.TType.Size * 3, sprite.TType.Size * 0);
				}
				if(!north && east && !southeast && south && !southwest && west) {
					vector = new Vector2(sprite.TType.Size * 4, sprite.TType.Size * 0);
				}
				if(north && !east && south && !southwest && west && !northwest) {
					vector = new Vector2(sprite.TType.Size * 5, sprite.TType.Size * 0);
				}
				if(north && !northeast && east && !southeast && south && !west) {
					vector = new Vector2(sprite.TType.Size * 4, sprite.TType.Size * 1);
				}
				if(north && !northeast && east && !south && west && !northwest) {
					vector = new Vector2(sprite.TType.Size * 5, sprite.TType.Size * 1);
				}
				if(!north && east && southeast && south && !southwest && west) {
					vector = new Vector2(sprite.TType.Size * 2, sprite.TType.Size * 4);
				}
				if(north && !east && south && southwest && west && !northwest) {
					vector = new Vector2(sprite.TType.Size * 3, sprite.TType.Size * 4);
				}
				if(!north && east && !southeast && south && southwest && west) {
					vector = new Vector2(sprite.TType.Size * 4, sprite.TType.Size * 4);
				}
				if(north && northeast && east && !south & west && northwest) {
					vector = new Vector2(sprite.TType.Size * 0, sprite.TType.Size * 0);
				}
				if(north && northeast && east && southeast && south && !west) {
					vector = new Vector2(sprite.TType.Size * 1, sprite.TType.Size * 0);
				}
				if(!north && east && southeast && south && southwest && west) {
					vector = new Vector2(sprite.TType.Size * 2, sprite.TType.Size * 0);
				}
				if(north && !east && south && southwest && west && northwest) {
					vector = new Vector2(sprite.TType.Size * 3, sprite.TType.Size * 0);
				}
				if(!north && east && !southeast && south && !southwest && west) {
					vector = new Vector2(sprite.TType.Size * 4, sprite.TType.Size * 0);
				}
				if(north && !east && south && !southwest && west && !northwest) {
					vector = new Vector2(sprite.TType.Size * 5, sprite.TType.Size * 0);
				}
				if(north && !northeast && east && !southeast && south && !west) {
					vector = new Vector2(sprite.TType.Size * 4, sprite.TType.Size * 1);
				}
				if(north && !northeast && east && !south && west && !northwest) {
					vector = new Vector2(sprite.TType.Size * 5, sprite.TType.Size * 1);
				}
				if(!north && east && southeast && south && !southwest && west) {
					vector = new Vector2(sprite.TType.Size * 2, sprite.TType.Size * 4);
				}
				if(north && !east && south && southwest && west && !northwest) {
					vector = new Vector2(sprite.TType.Size * 3, sprite.TType.Size * 4);
				}
				if(!north && east && !southeast && south && southwest && west) {
					vector = new Vector2(sprite.TType.Size * 4, sprite.TType.Size * 4);
				}
				if(north && !east && south && !southwest && west && northwest) {
					vector = new Vector2(sprite.TType.Size * 5, sprite.TType.Size * 4);
				}
				if(north && east && northeast && south && !southeast && !west) {
					vector = new Vector2(sprite.TType.Size * 2, sprite.TType.Size * 5);
				}
				if(north && !northeast && east && !south && west && northwest) {
					vector = new Vector2(sprite.TType.Size * 3, sprite.TType.Size * 5);
				}
				if(north && !northeast && east && southeast && south && !west) {
					vector = new Vector2(sprite.TType.Size * 4, sprite.TType.Size * 5);
				}
				if(north && northeast && east && !south && west && !northwest) {
					vector = new Vector2(sprite.TType.Size * 5, sprite.TType.Size * 5);
				}
				if(north && northeast && east && southeast && south && southwest && west && northwest) {
					vector = new Vector2(sprite.TType.Size * 6, sprite.TType.Size * 4);
				}
				if(north && northeast && east && !southeast && south && !southwest && west && northwest) {
					vector = new Vector2(sprite.TType.Size * 6, sprite.TType.Size * 0);
				}
				if(north && !northeast && east && southeast && south && !southwest && west && !northwest) {
					vector = new Vector2(sprite.TType.Size * 4, sprite.TType.Size * 2);
				}
				if(north && !northeast && east && !southeast && south && southwest && west && !northwest) {
					vector = new Vector2(sprite.TType.Size * 5, sprite.TType.Size * 2);
				}
				if(north && !northeast && east && southeast && south && southwest && west && !northwest) {
					vector = new Vector2(sprite.TType.Size * 6, sprite.TType.Size * 2);
				}
				if(north && northeast && east && southeast && south && !southwest && west && !northwest) {
					vector = new Vector2(sprite.TType.Size * 6, sprite.TType.Size * 1);
				}
				if(north && !northeast && east && !southeast && south && !southwest && west && !northwest) {
					vector = new Vector2(sprite.TType.Size * 3, sprite.TType.Size * 3);
				}
				if(north && northeast && east && !southeast && south && !southwest && west && !northwest) {
					vector = new Vector2(sprite.TType.Size * 4, sprite.TType.Size * 3);
				}
				if(north && !northeast && east && !southeast && south && !southwest && west && northwest) {
					vector = new Vector2(sprite.TType.Size * 5, sprite.TType.Size * 3);
				}
				if(north && !northeast && east && !southeast && south && southwest && west && northwest) {
					vector = new Vector2(sprite.TType.Size * 6, sprite.TType.Size * 3);
				}
				if(north && northeast && east && southeast && south && southwest && west && !northwest) {
					vector = new Vector2(sprite.TType.Size * 0, sprite.TType.Size * 6);
				}
				if(north && northeast && east && southeast && south && !southwest && west && northwest) {
					vector = new Vector2(sprite.TType.Size * 1, sprite.TType.Size * 6);
				}
				if(north && !northeast && east && southeast && south && southwest && west && northwest) {
					vector = new Vector2(sprite.TType.Size * 2, sprite.TType.Size * 6);
				}
				if(north && northeast && east && !southeast && south && southwest && west && northwest) {
					vector = new Vector2(sprite.TType.Size * 3, sprite.TType.Size * 6);
				}
				if(north && northeast && east && !southeast && south && southwest && west && !northwest) {
					vector = new Vector2(sprite.TType.Size * 4, sprite.TType.Size * 6);
				}
				if(north && !northeast && east && southeast && south && !southwest && west && northwest) {
					vector = new Vector2(sprite.TType.Size * 5, sprite.TType.Size * 6);
				}
				spriteBatch.Draw(sprite.Texture, 
				                 new Rectangle((int)pos.X, (int)pos.Y, sprite.TType.Size, sprite.TType.Size), 
				                 new Rectangle((int)vector.X, (int)vector.Y, sprite.TType.Size, sprite.TType.Size), 
				                 Color.White);
			}
			else {
				spriteBatch.Draw(sprite.Texture, new Vector2(pos.X, pos.Y), Color.White);

				if(entity.HasComp<ComponentSolid>() && DrawBorder) {
					if(!north) {
						for(int i = 0; i < sprite.TType.Size; i++) {
							spriteBatch.Draw(Art.GetPixel(), new Vector2(pos.X + i, pos.Y), Color.Black);
						}
					}
					if(!south) {
						for(int i = 0; i < sprite.TType.Size; i++) {
							spriteBatch.Draw(Art.GetPixel(), new Vector2(pos.X + i, pos.Y + sprite.TType.Size - 1), Color.Black);
						}
					}
					if(!east) {
						for(int i = 0; i < sprite.TType.Size; i++) {
							spriteBatch.Draw(Art.GetPixel(), new Vector2(pos.X + sprite.TType.Size - 1, pos.Y + i), Color.Black);
						}
					}
					if(!west) {
						for(int i = 0; i < sprite.TType.Size; i++) {
							spriteBatch.Draw(Art.GetPixel(), new Vector2(pos.X, pos.Y + i), Color.Black);
						}
					}
				}
			}
		}
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {
					typeof(ComponentTilesetSprite), 
					typeof(ComponentPosition)
				};
			}
		}
	}
}

