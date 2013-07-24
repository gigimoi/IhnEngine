//
// ISystem.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace IhnEngine {
	public interface ISystem {
		void Update(Entity entity);
		void Render(SpriteBatch spriteBatch, Entity entity);
		List<Type> RequiredComponents { get; }
	}
}

