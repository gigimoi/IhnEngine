using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace IhnLib {
    /// <summary>
    /// Example system to copypasta
    /// </summary>
	[Serializable]
	public class Default : ISystem{
        /// <summary>
        /// This system does not update
        /// </summary>
        /// <param name="ihn">Ihn entity is contained in</param>
        /// <param name="entity">Entity to update</param>
		public void Update(Ihn ihn, Entity entity) {
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
        /// Components this system needs to run on an entity
        /// </summary>
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {

				};
			}
		}
	}
}

