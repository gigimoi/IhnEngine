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
using IhnLib;

namespace IhnEngine {
	public class SystemRenderSprite : ISystem{
		public void Update(Ihn ihn, Entity entity) { }
		public void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity) {
			var pos = entity.GetComp<ComponentPosition>();
			var sprite = entity.GetComp<ComponentSprite>();
			spriteBatch.Draw(Rsc.Load<Texture2D>(sprite.Texture), new Vector2(pos.X, pos.Y), sprite.Source.ToRect(), Color.White);
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

