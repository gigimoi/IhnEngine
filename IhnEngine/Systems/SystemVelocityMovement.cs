//
// SystemVelocityMovement.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace IhnEngine {
	public class SystemVelocityMovement : ISystem{
		public void Update(Entity entity) {
			var pos = entity.GetComponent<ComponentPosition>();
			var velocity = entity.GetComponent<ComponentVelocity>();
			pos.X += velocity.X;
			pos.Y += velocity.Y;
		}

		public void Render(SpriteBatch spriteBatch, Entity entity) {
		}

		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {
					typeof(ComponentPosition), 
					typeof(ComponentVelocity)
				};
			}
		}
	}
}

