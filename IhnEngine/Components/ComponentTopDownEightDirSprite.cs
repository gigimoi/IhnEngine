//
// ComponentTopDownEightDirSprite.cs
using System;
using Microsoft.Xna.Framework.Graphics;

namespace IhnLib {
    /// <summary>
    /// Component to store texture of a top down 8-dir sprite
    /// </summary>
	[Serializable]
	public class ComponentTopDownEightDirSprite : Component{
        /// <summary>
        /// Path to the texture
        /// </summary>
		public string Texture;
	
        /// <summary>
        /// Instantiates a new ComponentTopDownEightDirSprite with texture
        /// </summary>
        /// <param name="texture">Path to texture</param>
		public ComponentTopDownEightDirSprite(string texture) {
			Texture = texture;
		}
	}
}

