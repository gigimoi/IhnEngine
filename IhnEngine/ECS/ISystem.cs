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

namespace IhnLib {
	public interface ISystem {
		void Update(Ihn ihn, Entity entity);
		void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity);
		List<Type> RequiredComponents { get; }
	}
}

