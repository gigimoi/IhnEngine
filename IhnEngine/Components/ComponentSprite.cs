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
	public class ComponentSprite : Component {
		public ComponentSprite(Texture2D texture) : this(texture, new Vector2(0, 0)) {
		}
		public ComponentSprite(Texture2D texture, Vector2 origin) : this(texture, new Rectangle(0, 0, texture.Width, texture.Height), origin) {
		}
		public ComponentSprite(Texture2D texture, Rectangle source, Vector2 origin) {
			Texture = texture;
			Source = source;
			Origin = origin;
		}
		public Texture2D Texture;
		public Rectangle Source;
		public Vector2 Origin;
	}
}

