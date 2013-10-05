using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
	public class ComponentMultiSprite : Component{
		public ComponentMultiSprite(List<string> textures, int layer) : this(textures, new Position(0, 0), layer) { }
		public ComponentMultiSprite(List<string> textures, Position origin, int layer) : this(textures, origin, 0, layer) { }
		public ComponentMultiSprite(List<string> textures, float rotation, int layer) : this(textures, new Position(0, 0), rotation, layer) { }
		public ComponentMultiSprite(List<string> textures, Position origin, float rotation, int layer) : this(textures, new FloatRect(0, 0, Rsc.Load<Texture2D>(textures[0]).Width, Rsc.Load<Texture2D>(textures[0]).Height), origin, rotation, layer) { }
		public ComponentMultiSprite(List<string> textures, FloatRect source, Position origin, float rotation, int layer) {
			Textures = textures;
			Source = source;
			Origin = origin;
			Rotation = rotation;
			Layer = layer;
		}

		public SpriteEffects Mirror = SpriteEffects.None;
		public List<string> Textures;
		public FloatRect Source;
		public Position Origin;
		public float Rotation;
		public float Layer;
	}
}
