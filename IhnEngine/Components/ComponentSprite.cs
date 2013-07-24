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

namespace IhnEngine {
	public class ComponentSprite : Component {
		public ComponentSprite(Texture2D texture, Rectangle source) {
			Texture = texture;
			Source = source;
		}
		public Texture2D Texture;
		public Rectangle Source;
	}
}

