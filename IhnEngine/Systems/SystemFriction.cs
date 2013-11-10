using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace IhnLib {
    /// <summary>
    /// Simulates a simple 2D air friction
    /// </summary>
	[Serializable]
	public class SystemFriction : ISystem{
        /// <summary>
        /// Global friction multiplier
        /// </summary>
		public float Multiplier = 0.65f;
        /// <summary>
        /// Multiplier for vertical slow
        /// </summary>
		public float VerticalMultiplier = 1f;
        /// <summary>
        /// Multiplier for horizontal slow
        /// </summary>
		public float HorizontalMultiplier = 1f;
        /// <summary>
        /// Updates the entity by reducing its velocity
        /// </summary>
        /// <param name="ihn">Ihn the entity is contained in</param>
        /// <param name="entity">Entity to slow</param>
		public void Update(Ihn ihn, Entity entity) {
			var velocity = entity.GetComp<ComponentVelocity>();
			velocity.X *= Multiplier / HorizontalMultiplier;
			velocity.Y *= Multiplier / VerticalMultiplier;
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
        /// Components required to run system on an entity, ComponentVelocity
        /// </summary>
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {
					typeof(ComponentVelocity)
				};
			}
		}
	}
}

