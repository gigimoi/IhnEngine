using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IhnLib {
    /// <summary>
    /// Aids in collision testing
    /// </summary>
	public class CollisionHelper {
        
        /// <summary>
        /// Simple 1 size fits all collision test
        /// </summary>
        /// <param name="ihn">Ihn to compare with</param>
        /// <param name="entity">Entity to compare</param>
        /// <returns>Whether the entity collides with any solid in Ihn</returns>
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

			//TODO: Optimize tilemap collision
			var tilemaps = ihn.GetEntitiesWith<ComponentTilemap>();
			if(tilemaps.Count > 0) {
				var tilemap = tilemaps[0].GetComp<ComponentTilemap>();
				for(int i = (int)Math.Max(0, (x - 250) / 32);
					i < (int)Math.Min(tilemap.Map.GetLength(0), (x + 250) / 32);
					i++) {
					for(int j = (int)Math.Max(0, (y - 250) / 32);
						j < (int)Math.Min(tilemap.Map.GetLength(1), (y + 250) / 32);
						j++) {
						if(tilemap.Map[i, j].Solid) {
							var tw = Rsc.Load<Texture2D>(tilemap.Map[i, j].RootTexture).Width;
							var th = Rsc.Load<Texture2D>(tilemap.Map[i, j].RootTexture).Height;
							var rect = new Rectangle(i * tw, j * th, tw, th);
							if(rect.Intersects(new Rectangle((int)x, (int)y, w, h))) {
								return true;
							}
						}
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
        /// <summary>
        /// Gets the boundries of an entity
        /// </summary>
        /// <param name="entity">Entity to get bounds for</param>
        /// <param name="pos">Position of the entity</param>
        /// <param name="x">outs the x coord to this</param>
        /// <param name="y">outs the y coord to this</param>
        /// <param name="w">outs the width to this</param>
        /// <param name="h">outs the height to this</param>
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

