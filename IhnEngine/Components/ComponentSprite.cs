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
		public ComponentSprite(string texture) : this(texture, new Position(0, 0)) { }
		public ComponentSprite(string texture, Position origin) : this(texture, origin, 0) { }
		public ComponentSprite(string texture, float rotation) : this(texture, new Position(0, 0), rotation) { }
		public ComponentSprite(string texture, Position origin, float rotation) : this(texture, new FloatRect(0, 0, Rsc.Load<Texture2D>(texture).Width, Rsc.Load<Texture2D>(texture).Height), origin, rotation) { }
		public ComponentSprite(string texture, FloatRect source, Position origin, float rotation) {
			Texture = texture;
			Source = source;
			Origin = origin;
			Rotation = rotation;
		}

		public SpriteEffects Mirror = SpriteEffects.None;
		public string Texture;
		public FloatRect Source;
		public Position Origin;
		public float Rotation;
	}
}

