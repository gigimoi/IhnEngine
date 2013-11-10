//
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IhnLib {
    /// <summary>
    /// Component that stores sprite data
    /// </summary>
	[Serializable]
	public class ComponentSprite : Component {
        /// <summary>
        /// Creates a new sprite
        /// </summary>
        /// <param name="texture">Path to texture</param>
        /// <param name="layer">Layer to render in</param>
		public ComponentSprite(string texture, int layer) : this(texture, new Position(0, 0), layer) { }
        /// <summary>
        /// Creates a new sprite
        /// </summary>
        /// <param name="texture">Path to texture</param>
        /// <param name="origin">Image origin</param>
        /// <param name="layer">Layer to render in</param>
		public ComponentSprite(string texture, Position origin, int layer) : this(texture, origin, 0, layer) { }
        /// <summary>
        /// Creates a new sprite
        /// </summary>
        /// <param name="texture">Path to texture</param>
        /// <param name="rotation">Angle to render image at (In degrees)</param>
        /// <param name="layer">Layer to render in</param>
		public ComponentSprite(string texture, float rotation, int layer) : this(texture, new Position(0, 0), rotation, layer) { }
        /// <summary>
        /// Creates a new sprite
        /// </summary>
        /// <param name="texture">Path to texture</param>
        /// <param name="origin">Image origin</param>
        /// <param name="rotation">Angle to render image at (In degrees)</param>
        /// <param name="layer">Layer to render in</param>
        public ComponentSprite(string texture, Position origin, float rotation, int layer) : this(texture, new FloatRect(0, 0, Rsc.Load<Texture2D>(texture).Width, Rsc.Load<Texture2D>(texture).Height), origin, rotation, layer) { }
        /// <summary>
        /// Creates a new sprite
        /// </summary>
        /// <param name="texture">Path to texture</param>
        /// <param name="source">Rectangle to slice out of loaded texture</param>
        /// <param name="origin">Image origin</param>
        /// <param name="rotation">Angle to render image at (In degrees)</param>
        /// <param name="layer">Layer to render in</param>
        public ComponentSprite(string texture, FloatRect source, Position origin, float rotation, int layer) {
			Texture = texture;
			Source = source;
			Origin = origin;
			Rotation = rotation;
			Layer = layer;
		}
        /// <summary>
        /// Sprite Effects to render with
        /// </summary>
		public SpriteEffects Mirror = SpriteEffects.None;
        /// <summary>
        /// Path to texture to render
        /// </summary>
		public string Texture;
        /// <summary>
        /// Rectangle to cut out of sprite
        /// </summary>
		public FloatRect Source;
        /// <summary>
        /// Origin of the sprite
        /// </summary>
		public Position Origin;
        /// <summary>
        /// Angle to render sprite at (In degrees)
        /// </summary>
		public float Rotation;
        /// <summary>
        /// Layer to render sprite in
        /// </summary>
		public float Layer;
	}
}

