//
// SystemDeath.cs
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
	public class SystemDeath : ISystem{
		public void Update(Ihn ihn, Entity entity) {
			var health = entity.GetComp<ComponentHealth>();

			if(health.Hp <= 0) {
				ihn.RemoveEntity(entity);
			}
		}
		public void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity) {
		}
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {
					typeof(ComponentHealth)
				};
			}
		}
	}
}

