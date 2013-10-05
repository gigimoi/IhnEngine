//
// ComponentSprite.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IhnLib {
	[Serializable]
	public class ComponentSprite : Component {
		public ComponentSprite(string texture, int layer) : this(texture, new Position(0, 0), layer) { }
		public ComponentSprite(string texture, Position origin, int layer) : this(texture, origin, 0, layer) { }
		public ComponentSprite(string texture, float rotation, int layer) : this(texture, new Position(0, 0), rotation, layer) { }
		public ComponentSprite(string texture, Position origin, float rotation, int layer) : this(texture, new FloatRect(0, 0, Rsc.Load<Texture2D>(texture).Width, Rsc.Load<Texture2D>(texture).Height), origin, rotation, layer) { }
		public ComponentSprite(string texture, FloatRect source, Position origin, float rotation, int layer) {
			Texture = texture;
			Source = source;
			Origin = origin;
			Rotation = rotation;
			Layer = layer;
		}

		public SpriteEffects Mirror = SpriteEffects.None;
		public string Texture;
		public FloatRect Source;
		public Position Origin;
		public float Rotation;
		public float Layer;
	}
}

