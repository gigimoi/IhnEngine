//
// SystemKeyboardMovement.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace IhnLib {
	public class SystemKeyboardMovement : ISystem{
		public void Update(Ihn ihn, Entity entity) { 
			var pos = entity.GetComp<ComponentPosition>();
			var move = entity.GetComp<ComponentKeyboardMovement>();
			ComponentVelocity velocity = null;
			if(entity.HasComp<ComponentVelocity>()) {
				velocity = entity.GetComp<ComponentVelocity>();
			}

			bool n;
			bool e;
			bool s;
			bool w;

			n = KeyHelper.KeyDown(Keys.W);
			e = KeyHelper.KeyDown(Keys.D);
			s = KeyHelper.KeyDown(Keys.S);
			w = KeyHelper.KeyDown(Keys.A);

			Vector2 momentum = new Vector2();
			if(velocity == null) {
				pos.X += e ? move.Speed * move.HorizantalModifier : 0;
				momentum.X += e ? move.Speed * move.HorizantalModifier : 0;
				if(CollisionHelper.Colliding(ihn, entity)) {
					pos.X -= e ? move.Speed * move.HorizantalModifier : 0;
				}
				pos.X += w ? -move.Speed * move.HorizantalModifier : 0;
				momentum.X += w ? -move.Speed * move.HorizantalModifier : 0;
				if(CollisionHelper.Colliding(ihn, entity)) {
					pos.X -= w ? -move.Speed * move.HorizantalModifier : 0;
				}
				pos.Y += s ?  move.Speed * move.VerticalModifier : 0;
				momentum.Y += s ? move.Speed * move.VerticalModifier : 0;
				if(CollisionHelper.Colliding(ihn, entity)) {
					pos.Y -= s ? move.Speed * move.VerticalModifier : 0;
				}
				pos.Y += n ? -move.Speed * move.VerticalModifier : 0;
				momentum.Y += n ? -move.Speed * move.VerticalModifier : 0;
				if(CollisionHelper.Colliding(ihn, entity)) {
					pos.Y -= n ? -move.Speed * move.VerticalModifier : 0;
				}
			}
			else {
				velocity.X += e ? move.Speed * move.HorizantalModifier : 0;
				momentum.X += e ? move.Speed * move.HorizantalModifier : 0;
				velocity.X += w ? -move.Speed * move.HorizantalModifier : 0;
				momentum.X += w ? -move.Speed * move.HorizantalModifier : 0;
				velocity.Y += s ?  move.Speed * move.VerticalModifier : 0;
				momentum.Y += s ? move.Speed * move.VerticalModifier : 0;
				velocity.Y += n ? -move.Speed * move.VerticalModifier : 0;
				momentum.Y += n ? -move.Speed * move.VerticalModifier : 0;
			}
			if(entity.HasComp<ComponentDirection>() && (momentum.X != 0 || momentum.Y != 0)) {
				var dir = entity.GetComp<ComponentDirection>();
				dir.Dir = DirectionHelper.VectorToDirection(momentum);
			}
		}
		public void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity) { }
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() { 
					typeof(ComponentPosition),
					typeof(ComponentKeyboardMovement)
				};
			}
		}
	}
}

