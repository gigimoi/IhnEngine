//
// SystemFollow.cs
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
	public class SystemFollow : ISystem{
		public void Update(Ihn ihn, Entity entity) {
			var pos = entity.GetComp<ComponentPosition>();
			var follow = entity.GetComp<ComponentFollow>();

			if(follow.ToFollow.HasComp<ComponentPosition>()) {
				pos.X = follow.ToFollow.GetComp<ComponentPosition>().X + follow.Offset.X;
				pos.Y = follow.ToFollow.GetComp<ComponentPosition>().Y + follow.Offset.Y;
			}
		}
		public void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity) {
		}
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {
					typeof(ComponentPosition),
					typeof(ComponentFollow)
				};
			}
		}
	}
}

