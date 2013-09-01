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

namespace IhnLib {
	[Serializable]
	public class SystemRenderSprite : ISystem{
		public void Update(Ihn ihn, Entity entity) { }
		public void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity) {
			var pos = entity.GetComp<ComponentPosition>();
			var sprite = entity.GetComp<ComponentSprite>();
			if(entity.HasComp<ComponentSize>()) {
				var size = entity.GetComp<ComponentSize>();

				spriteBatch.Draw(Rsc.Load<Texture2D>(sprite.Texture), new Rectangle((int)pos.X, (int)pos.Y, size.Width, size.Height), sprite.Source.ToRect(), Color.White, 0, sprite.Origin, SpriteEffects.None, 0);
			}
			else {
				spriteBatch.Draw(Rsc.Load<Texture2D>(sprite.Texture), new Rectangle((int)pos.X, (int)pos.Y, (int)sprite.Source.Width, (int)sprite.Source.Height), sprite.Source.ToRect(), Color.White, 0, sprite.Origin, SpriteEffects.None, 0);
			}
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

