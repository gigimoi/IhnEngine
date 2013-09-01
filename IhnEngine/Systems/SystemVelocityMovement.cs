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
	public class SystemVelocityMovement : ISystem{
		public void Update(Ihn ihn, Entity entity) {
			var pos = entity.GetComp<ComponentPosition>();
			var velocity = entity.GetComp<ComponentVelocity>();
			if((velocity.X != 0 || velocity.Y != 0)) {
				if(Math.Abs(velocity.X) > Math.Abs(velocity.Y)) {
					pos.X += velocity.X;
					if(CollisionHelper.Colliding(ihn, entity)) {
						while(CollisionHelper.Colliding(ihn, entity)) {
							pos.X -= velocity.X / 6f;
						}
					}
					pos.Y += velocity.Y;
					if(CollisionHelper.Colliding(ihn, entity)) {
						while(CollisionHelper.Colliding(ihn, entity)) {
							pos.Y -= velocity.Y / 6f;
						}
					}
				}
				else 
				{
					pos.Y += velocity.Y;
					if(CollisionHelper.Colliding(ihn, entity)) {
						while(CollisionHelper.Colliding(ihn, entity)) {
							pos.Y -= velocity.Y / 6f;
						}
					}
					pos.X += velocity.X;
					if(CollisionHelper.Colliding(ihn, entity)) {
						while(CollisionHelper.Colliding(ihn, entity)) {
							pos.X -= velocity.X / 6f;
						}
					}
				}
				pos.Y = (int)pos.Y;
				pos.X = (int)pos.X;
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