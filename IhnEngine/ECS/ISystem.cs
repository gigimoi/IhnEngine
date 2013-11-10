using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace IhnLib {
    /// <summary>
    /// Extended to operate on entities matching a list of components, see DefaultSystem in Systems
    /// </summary>
	public interface ISystem {
        /// <summary>
        /// This system does not update, override this to allow updating
        /// </summary>
        /// <param name="ihn">Ihn entity is contained in</param>
        /// <param name="entity">Entity to update</param>
		void Update(Ihn ihn, Entity entity);
        /// <summary>
        /// This system does not render, override this to allow rendering
        /// </summary>
        /// <param name="ihn">Ihn entity is contained in</param>
        /// <param name="spriteBatch">Spritebatch to render with</param>
        /// <param name="entity">Entity to update</param>
		void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity);
        /// <summary>
        /// List of components required to update/render
        /// </summary>
		List<Type> RequiredComponents { get; }
	}
}

