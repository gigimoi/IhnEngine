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
	[Serializable]
	public class ComponentTopDownEightDirSprite : Component{
		public string Texture;
	
		public ComponentTopDownEightDirSprite(string texture) {
			Texture = texture;
		}
	}
}

