//
// SystemRenderTopDownEightDirSprite.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace IhnLib {
	public class SystemRenderTopDownEightDirSprite : ISystem{
		public void Update(Ihn ihn, Entity entity) {
		}
		public void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity) {
			var sprite = entity.GetComp<ComponentTopDownEightDirSprite>();
			var pos = entity.GetComp<ComponentPosition>();
			var dir = entity.GetComp<ComponentDirection>();
			int sizex = sprite.Texture.Width / 4;
			int sizey = sprite.Texture.Height / 2;
			if(entity.HasComp<ComponentSize>()) {
				var size = entity.GetComp<ComponentSize>();
				sizex = size.Width;
				sizey = size.Height;
			}
			float Rotation = DirectionHelper.ToAngle(dir.Dir);
			spriteBatch.Draw(sprite.Texture, 
			                 new Rectangle((int)pos.X, (int)pos.Y, sizex, sizey),
			                 new Rectangle(DirectionHelper.IsDiagonal(dir.Dir) ? sprite.Texture.Width / 2 : 0, 0, sprite.Texture.Width / 2, sprite.Texture.Height), 
			                 Color.White, 
			                 MathHelper.ToRadians(Rotation - (DirectionHelper.IsDiagonal(dir.Dir) ? 45 : 0)),
			                 new Vector2(sprite.Texture.Width / 4, sprite.Texture.Height / 2), 
			                 SpriteEffects.None, 
			                 0);
		}
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {
					typeof(ComponentTopDownEightDirSprite), 
					typeof(ComponentPosition),
					typeof(ComponentDirection)
				};
			}
		}
	}
}

