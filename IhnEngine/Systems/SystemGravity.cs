//
// SystemGravity.cs
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
	[Serializable]
	public class SystemGravity : ISystem{
		public void Update(Ihn ihn, Entity entity) {
			if(entity.HasComp<ComponentSize>() || entity.HasComp<ComponentSprite>()) {
				var pos = entity.GetComp<ComponentPosition>();
				var gravity = entity.GetComp<ComponentGravity>();
				var velocity = entity.GetComp<ComponentVelocity>();
				velocity.Y += gravity.Multiplier * ComponentGravity.GlobalMultiplier;
			}
		}
		public void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity) {
		}
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {
					typeof(ComponentPosition),
					typeof(ComponentGravity),
					typeof(ComponentVelocity)
				};
			}
		}
	}
}
