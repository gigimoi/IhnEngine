using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
    /// <summary>
    /// Component to render multiple sprites at once
    /// </summary>
    [Serializable]
	public class ComponentMultiSprite : Component{
        /// <summary>
        /// Creates a new component with textures in layer
        /// </summary>
        /// <param name="textures">List of paths to textures</param>
        /// <param name="layer">Layer to render at</param>
		public ComponentMultiSprite(List<string> textures, int layer) : this(textures, new Position(0, 0), layer) { }
		/// <summary>
		/// Creates a new component with textures at origin in layer
		/// </summary>
		/// <param name="textures">List of paths to textures</param>
        /// <param name="origin">Position origin</param>
		/// <param name="layer">Layer to render at</param>
        public ComponentMultiSprite(List<string> textures, Position origin, int layer) : this(textures, origin, 0, layer) { }
        /// <summary>
        /// Creates a new component with textures with rotation in layer
        /// </summary>
        /// <param name="textures">List of paths to textures</param>
        /// <param name="rotation">Angle to draw the sprite at (In degrees)</param>
        /// <param name="layer">Layer to render at</param>
        public ComponentMultiSprite(List<string> textures, float rotation, int layer) : this(textures, new Position(0, 0), rotation, layer) { }
        /// <summary>
        /// Creates a new component with textures at origin with rotation in layer
        /// </summary>
        /// <param name="textures">List of paths to textures</param>
        /// <param name="origin">Position origin</param>
        /// <param name="rotation">Angle to draw the sprite at (In degrees)</param>
        /// <param name="layer">Layer to render at</param>
        public ComponentMultiSprite(List<string> textures, Position origin, float rotation, int layer) : this(textures, new FloatRect(0, 0, Rsc.Load<Texture2D>(textures[0]).Width, Rsc.Load<Texture2D>(textures[0]).Height), origin, rotation, layer) { }
        /// <summary>
        /// Creates a new component with textures pulled from source rectangle at origin with rotation in layer
        /// </summary>
        /// <param name="textures">List of paths to textures</param>
        /// <param name="source">Rect to pull images from</param>
        /// <param name="origin">Position origin</param>
        /// <param name="rotation">Angle to draw the sprite at (In degrees)</param>
        /// <param name="layer">Layer to render at</param>
        public ComponentMultiSprite(List<string> textures, FloatRect source, Position origin, float rotation, int layer) {
			Textures = textures;
			Source = source;
			Origin = origin;
			Rotation = rotation;
			Layer = layer;
		}
        /// <summary>
        /// Spriteeffect to apply on rendering
        /// </summary>
		public SpriteEffects Mirror = SpriteEffects.None;
        /// <summary>
        /// List of textures to render
        /// </summary>
		public List<string> Textures;
        /// <summary>
        /// Rectangle to render from
        /// </summary>
		public FloatRect Source;
        /// <summary>
        /// Origin of the sprites
        /// </summary>
		public Position Origin;
        /// <summary>
        /// Angle to draw the sprite at (In degrees)
        /// </summary>
		public float Rotation;
        /// <summary>
        /// Layer to draw sprites in
        /// </summary>
		public float Layer;
	}
}
