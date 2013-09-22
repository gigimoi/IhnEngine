using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
	public class ComponentMultiSprite : Component{
		public ComponentMultiSprite(List<string> textures) : this(textures, new Vector2(0, 0)) { }
		public ComponentMultiSprite(List<string> textures, Vector2 origin) : this(textures, origin, 0) { }
		public ComponentMultiSprite(List<string> textures, float rotation) : this(textures, new Vector2(0, 0), rotation) { }
		public ComponentMultiSprite(List<string> textures, Vector2 origin, float rotation) : this(textures, new FloatRect(0, 0, Rsc.Load<Texture2D>(textures[0]).Width, Rsc.Load<Texture2D>(textures[0]).Height), origin, rotation) { }
		public ComponentMultiSprite(List<string> textures, FloatRect source, Vector2 origin, float rotation) {
			Textures = textures;
			Source = source;
			Origin = origin;
			Rotation = rotation;
		}

		public SpriteEffects Mirror = SpriteEffects.None;
		public List<string> Textures;
		public FloatRect Source;
		public Vector2 Origin;
		public float Rotation;
	}
}
