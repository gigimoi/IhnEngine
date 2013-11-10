using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace IhnLib {
    /// <summary>
    /// Applies a simple gravity physic to an entity
    /// </summary>
	[Serializable]
	public class SystemGravity : ISystem{
        /// <summary>
        /// Updates the Y velocity of an entity
        /// </summary>
        /// <param name="ihn">Ihn entity is contained in</param>
        /// <param name="entity">Entity to update</param>
		public void Update(Ihn ihn, Entity entity) {
			if(entity.HasComp<ComponentSize>() || entity.HasComp<ComponentSprite>()) {
				var pos = entity.GetComp<ComponentPosition>();
				var gravity = entity.GetComp<ComponentGravity>();
				var velocity = entity.GetComp<ComponentVelocity>();
				velocity.Y += gravity.Multiplier * ComponentGravity.GlobalMultiplier;
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
        /// Components required to run this system on an entity: ComponentGravity, ComponentPosition, ComponentVelocity
        /// </summary>
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {
					typeof(ComponentPosition),
					typeof(ComponentGravity),
					typeof(ComponentVelocity)
				};
			}
		}
	}
}

