using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace IhnLib {
    /// <summary>
    /// Moves an entity based on its velocity
    /// </summary>
	[Serializable]
	public class SystemVelocityMovement : ISystem{
        /// <summary>
        /// Moves the entity contained in Ihn
        /// </summary>
        /// <param name="ihn">Ihn entity is contained in</param>
        /// <param name="entity">Entity to accelerate</param>
		public void Update(Ihn ihn, Entity entity) {
			var pos = entity.GetComp<ComponentPosition>();
			var velocity = entity.GetComp<ComponentVelocity>();
			if((velocity.X != 0 || velocity.Y != 0)) {
				if(Math.Abs(velocity.X) > Math.Abs(velocity.Y)) {
					MoveX(ihn, entity, pos, velocity);
					MoveY(ihn, entity, pos, velocity);
				}
				else 
				{
					MoveY(ihn, entity, pos, velocity);
					MoveX(ihn, entity, pos, velocity);
				}
			}
		}

		static void MoveX(Ihn ihn, Entity entity, ComponentPosition pos, ComponentVelocity velocity) {
			pos.X += velocity.X;
			if(CollisionHelper.Colliding(ihn, entity)) {
				pos.X -= velocity.X;
				int runs = 0;
				while(!CollisionHelper.Colliding(ihn, entity) && runs < Math.Abs(velocity.X) + 2) {
					pos.X += velocity.X > 0 ? 1 : -1;
					runs++;
				}
				pos.X -= velocity.X > 0 ? 1 : -1;
			}
		}

		static void MoveY(Ihn ihn, Entity entity, ComponentPosition pos, ComponentVelocity velocity) {
			pos.Y += velocity.Y;
			if(CollisionHelper.Colliding(ihn, entity)) {
				pos.Y -= velocity.Y;
				int runs = 0;
				while(!CollisionHelper.Colliding(ihn, entity) && runs < Math.Abs(velocity.Y) + 2) {
					runs++;
					pos.Y += velocity.Y > 0 ? 1 : -1;
				}
				pos.Y -= velocity.Y > 0 ? 1 : -1;
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
        /// Components required to run system on an entity. ComponentPosition and ComponentVelocity
        /// </summary>
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {
					typeof(ComponentPosition), 
					typeof(ComponentVelocity)
				};
			}
		}
	}
}