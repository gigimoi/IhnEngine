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

namespace IhnLib {
	[Serializable]
	public class SystemVelocityMovement : ISystem{
		public void Update(Ihn ihn, Entity entity) {
			var pos = entity.GetComp<ComponentPosition>();
			var velocity = entity.GetComp<ComponentVelocity>();
			if((velocity.X != 0 || velocity.Y != 0)) {
				if(Math.Abs(velocity.X) > Math.Abs(velocity.Y)) {
					MoveX(ihn, entity, pos, velocity);
					MoveY(ihn, entity, pos, velocity);
				}
				else 
				{
					MoveY(ihn, entity, pos, velocity);
					MoveX(ihn, entity, pos, velocity);
				}
			}
		}

		static void MoveX(Ihn ihn, Entity entity, ComponentPosition pos, ComponentVelocity velocity) {
			pos.X += velocity.X;
			if(CollisionHelper.Colliding(ihn, entity)) {
				pos.X -= velocity.X;
				int runs = 0;
				while(!CollisionHelper.Colliding(ihn, entity) && runs < 10) {
					pos.X += velocity.X / 10f;
					runs++;
				}
				pos.X -= velocity.X / 10f;
			}
		}

		static void MoveY(Ihn ihn, Entity entity, ComponentPosition pos, ComponentVelocity velocity) {
			pos.Y += velocity.Y;
			if(CollisionHelper.Colliding(ihn, entity)) {
				pos.Y -= velocity.Y;
				int runs = 0;
				while(!CollisionHelper.Colliding(ihn, entity) && runs < 10) {
					runs++;
					pos.Y += velocity.Y / 10f;
				}
				pos.Y -= velocity.Y / 10f;
			}
		}

		public void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity) {
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