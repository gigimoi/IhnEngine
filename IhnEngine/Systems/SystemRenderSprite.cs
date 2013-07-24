//
// SystemRenderSprite.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IhnEngine {
	public class SystemRenderSprite : ISystem{
		public void Update(Entity entity) { }
		public void Render(SpriteBatch spriteBatch, Entity entity) {
			var pos = entity.GetComponent<ComponentPosition>();
			var sprite = entity.GetComponent<ComponentSprite>();
			spriteBatch.Draw(sprite.Texture, new Vector2(pos.X, pos.Y), sprite.Source, Color.White);
		}
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() { 
					typeof(ComponentPosition), 
					typeof(ComponentSprite)
				};
			}
		}
	}
}

