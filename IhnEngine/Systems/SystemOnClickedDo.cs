//
// SystemOnClickedDo.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace IhnLib {
	[Serializable]
	public class SystemOnClickedDo : ISystem{
		public void Update(Ihn ihn, Entity entity) {
			var pos = entity.GetComp<ComponentPosition>();
			var x = pos.X;
			var y = pos.Y;
			var w = 0;
			var h = 0;
			if(entity.HasComp<ComponentAABB>()) {
				var aabb = entity.GetComp<ComponentAABB>();
				x = aabb.X;
				y = aabb.Y;
				w = aabb.Width;
				h = aabb.Height;
			}
			else if(entity.HasComp<ComponentTopDownEightDirSprite>()) {
				var spr = entity.GetComp<ComponentTopDownEightDirSprite>();
				x -= Rsc.Load<Texture2D>(spr.Texture).Width / 8;
				y -= Rsc.Load<Texture2D>(spr.Texture).Height / 4;
				w = Rsc.Load<Texture2D>(spr.Texture).Width / 4;
				h = Rsc.Load<Texture2D>(spr.Texture).Height / 2;
			}
			else if(entity.HasComp<ComponentSize>()) {
				w = entity.GetComp<ComponentSize>().Width;
				h = entity.GetComp<ComponentSize>().Height;
			}
			else if(entity.HasComp<ComponentSprite>()) {
				var spr = entity.GetComp<ComponentSprite>();
				x -= spr.Origin.X;
				y -= spr.Origin.Y;
				w = Rsc.Load<Texture2D>(spr.Texture).Width;
				h = Rsc.Load<Texture2D>(spr.Texture).Height;
			}
			var onclick = entity.GetComp<ComponentOnClickedDo>();
			if(MouseHelper.MouseLeftPressed()) {
				if(MouseHelper.X > x && MouseHelper.X < x + w) {
					if(MouseHelper.Y > y && MouseHelper.Y < y + h) {
						onclick.Todo.Invoke(this, new EventArgs());
					}
				}
			}
		}
		public void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity) {
		}
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {
					typeof(ComponentOnClickedDo),
					typeof(ComponentPosition)
				};
			}
		}
	}
}

