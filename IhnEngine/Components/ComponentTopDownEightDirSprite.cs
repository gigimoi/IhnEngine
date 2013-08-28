//
// ComponentTopDownEightDirSprite.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using Microsoft.Xna.Framework.Graphics;

namespace IhnLib {
	public class ComponentTopDownEightDirSprite : Component{
		public Texture2D Texture;
	
		public ComponentTopDownEightDirSprite(Texture2D texture) {
			Texture = texture;
		}
	}
}

