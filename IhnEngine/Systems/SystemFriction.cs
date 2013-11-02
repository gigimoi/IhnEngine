//
// SystemFriction.cs
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
	public class SystemFriction : ISystem{
		public float Multiplier = 0.65f;
		public float VerticalMultiplier = 1f;
		public float HorizontalMultiplier = 1f;
		public void Update(Ihn ihn, Entity entity) {
			var velocity = entity.GetComp<ComponentVelocity>();
			velocity.X *= Multiplier / HorizontalMultiplier;
			velocity.Y *= Multiplier / VerticalMultiplier;
		}
		public void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity) {
		}
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {
					typeof(ComponentVelocity)
				};
			}
		}
	}
}

