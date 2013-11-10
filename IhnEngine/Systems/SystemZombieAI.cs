using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace IhnLib {
    /// <summary>
    /// Horrifically broken
    /// </summary>
	[Serializable]
	public class SystemZombieAI : ISystem{
        /// <summary>
        /// Entity to chase after
        /// </summary>
		public Entity Following;
		//TODO: Fix zombie AI for new tilemap system
		//public SystemTilemap Map
        /// <summary>
        /// Instantiates a new system for zombie AI
        /// </summary>
        /// <param name="following">Entity to chase after</param>
		public SystemZombieAI(Entity following/*, SystemTilemap map*/) {
			EventManager.Listen("Tilemap Changed", shouldRefreshPaths);
			Following = following;
			//Map = map;
		}
		static void shouldRefreshPaths(object sender, EventArgs e) {
			var entities = Ihn.Instance.GetEntitiesWith<ComponentZombieAI>();
			for(int i = 0; i < entities.Count; i++) {
				entities[i].GetComp<ComponentZombieAI>().ShouldRefreshPath = true;
			}
		}
        /// <summary>
        /// Chases after the entity using a 2D pathmap
        /// </summary>
        /// <param name="ihn">Ihn entity is contained in</param>
        /// <param name="entity">Entity to update</param>
		public void Update(Ihn ihn, Entity entity) {
			var pos = entity.GetComp<ComponentPosition>();
			var ai = entity.GetComp<ComponentZombieAI>();
			if(ai.SwipeTime > 0) {
				ai.SwipeTime--;
			}
			if(ai.ChaseX != Following.GetComp<ComponentPosition>().X / ai.TileSize || ai.ChaseY != Following.GetComp<ComponentPosition>().Y / ai.TileSize) {
				ai.ShouldRefreshPath = true;
				ai.ChaseX = (int)Following.GetComp<ComponentPosition>().X / ai.TileSize;
				ai.ChaseY = (int)Following.GetComp<ComponentPosition>().Y / ai.TileSize;
			}
			if(ai.ShouldRefreshPath) {
				ai.ShouldRefreshPath = false;
				//ai.Path = Pathfinding.FindPath(Map.SolidityMap, new Vector2(pos.X / ai.TileSize, pos.Y / ai.TileSize), new Vector2(ai.ChaseX, ai.ChaseY));
			}
			Vector2 nextPos;
			if(ai.Path.Count > 1) {
				nextPos = ai.Path[ai.PathStep + 1] * ai.TileSize + new Vector2(ai.TileSize / 2, ai.TileSize / 2);
			}
			else {
				nextPos = new Vector2(Following.GetComp<ComponentPosition>().X, Following.GetComp<ComponentPosition>().Y);
			}
			Vector2 momentum = new Vector2(0, 0);
			if(nextPos.X > pos.X) {
				pos.X += ai.Speed;
				momentum.X++;
			}
			if(nextPos.Y < pos.Y) {
				pos.Y -= ai.Speed;
				momentum.Y--;
			}
			if(nextPos.X < pos.X) {
				pos.X -= ai.Speed;
				momentum.X--;
			}
			if(nextPos.Y > pos.Y) {
				pos.Y += ai.Speed;
				momentum.Y++;
			}
			if(entity.HasComp<ComponentDirection>()) {
				entity.GetComp<ComponentDirection>().Dir = DirectionHelper.VectorToDirection(momentum);
			}
		}
        /// <summary>
        /// This system does not render
        /// </summary>
        /// <param name="ihn">Ihn entity is contained in</param>
        /// <param name="spriteBatch">Spritebatch to draw with</param>
        /// <param name="entity">Entity to draw</param>
		public void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity) {
		}
        /// <summary>
        /// Components required for an entity to be acted upon by this system, ComponentPosition and ComponentZombieAI
        /// </summary>
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {
					typeof(ComponentPosition), 
					typeof(ComponentZombieAI)
				};
			}
		}
	}
}

