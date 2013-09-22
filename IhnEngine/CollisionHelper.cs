//
// CollisionHelper.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IhnLib {
	public class CollisionHelper {
		public static bool Colliding(Ihn ihn, Entity entity) {
			if(!entity.HasComp<ComponentSolid>()) {
				return false;
			}
			var pos = entity.GetComp<ComponentPosition>();
			float x;
			float y;
			int w;
			int h;
			GetBounds(entity, pos, out x, out y, out w, out h);

			var tiles = ihn.GetEntitiesWith<ComponentTilesetSprite>();
			for(int i = 0; i < tiles.Count; i++) {
				if(tiles[i].HasComp<ComponentSolid>()) {
					var tilePos = tiles[i].GetComp<ComponentPosition>();
					var tileSprite = tiles[i].GetComp<ComponentTilesetSprite>();
					if(new Rectangle((int)tilePos.X, (int)tilePos.Y, tileSprite.TType.Size, tileSprite.TType.Size).Intersects(new Rectangle((int)x, (int)y, w, h))) {
						return true;
					}
				}
			}

			if(entity.HasComp<ComponentVelocity>() && !entity.HasComp<ComponentTransient>()) {
				var velocity = entity.GetComp<ComponentVelocity>();
				var bounds = new Rectangle((int)x, (int)y, (int)w, (int)h);
				var entities = ihn.GetEntitiesWith<ComponentSolid, ComponentPosition>();
				for(int i = 0; i < entities.Count; i++) {
					if(entities[i] != entity && !entities[i].HasComp<ComponentTransient>()) {
						var entsize = new Vector2(1, 1);
						if(!entities[i].HasComp<ComponentPosition>()) {
							continue;
						}
						if(entities[i].HasComp<ComponentSize>()) {
							entsize = new Vector2(entities[i].GetComp<ComponentSize>().Width, entities[i].GetComp<ComponentSize>().Height);
						}
						else if(entities[i].HasComp<ComponentSprite>()) {
							entsize = new Vector2(Rsc.Load<Texture2D>(entities[i].GetComp<ComponentSprite>().Texture).Width, Rsc.Load<Texture2D>(entities[i].GetComp<ComponentSprite>().Texture).Height);
						}
						var entbounds = new Rectangle((int)entities[i].GetComp<ComponentPosition>().X, (int)entities[i].GetComp<ComponentPosition>().Y, (int)entsize.X, (int)entsize.Y);
						if(entbounds.Intersects(bounds)) {
							return true;
						}
					}
				}
			}
			return false;
		}

		public static void GetBounds(Entity entity, ComponentPosition pos, out float x, out float y, out int w, out int h) {
			x = pos.X;
			y = pos.Y;
			w = 0;
			h = 0;
			if(entity.HasComp<ComponentAABB>()) {
				var aabb = entity.GetComp<ComponentAABB>();
				x = aabb.X;
				y = aabb.Y;
				w = aabb.Width;
				h = aabb.Height;
			}
			else if(entity.HasComp<ComponentTopDownEightDirSprite>()) {
				var spr = entity.GetComp<ComponentTopDownEightDirSprite>();
				x -= Rsc.Load<Texture2D>(spr.Texture).Width / 8;
				y -= Rsc.Load<Texture2D>(spr.Texture).Height / 4;
				w = Rsc.Load<Texture2D>(spr.Texture).Width / 4;
				h = Rsc.Load<Texture2D>(spr.Texture).Height / 2;
			}
			else if(entity.HasComp<ComponentSize>()) {
				w = entity.GetComp<ComponentSize>().Width;
				h = entity.GetComp<ComponentSize>().Height;
			}
			else if(entity.HasComp<ComponentSprite>()) {
				var spr = entity.GetComp<ComponentSprite>();
				x -= spr.Origin.X;
				y -= spr.Origin.Y;
				w = Rsc.Load<Texture2D>(spr.Texture).Width;
				h = Rsc.Load<Texture2D>(spr.Texture).Height;
			}
		}
	}
}

