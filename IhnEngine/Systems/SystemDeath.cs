using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace IhnLib {
    /// <summary>
    /// Destroys any entity with no health
    /// </summary>
	[Serializable]
	public class SystemDeath : ISystem{
        /// <summary>
        /// Checks if an entities HP is lower than or equal to 0 and removes it if so
        /// </summary>
        /// <param name="ihn">Ihn calling the function</param>
        /// <param name="entity">Entity called on</param>
		public void Update(Ihn ihn, Entity entity) {
			var health = entity.GetComp<ComponentHealth>();

			if(health.Hp <= 0) {
				ihn.RemoveEntity(entity);
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
        /// Required components to run this system on an entity, ComponentHealth
        /// </summary>
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {
					typeof(ComponentHealth)
				};
			}
		}
	}
}

