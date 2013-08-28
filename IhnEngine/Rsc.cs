//
// Rsc.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace IhnLib {
	public static class Rsc {
		static Dictionary<Type, Dictionary<string, object>> rscs = new Dictionary<Type, Dictionary<string, object>>();
		public static T Load<T>(string path) {
			if(!rscs.ContainsKey(typeof(T))) {
				rscs.Add(typeof(T), new Dictionary<string, object>());
			}
			if(!rscs[typeof(T)].ContainsKey(path)) {
				rscs[typeof(T)].Add(path, Ihn.Instance.Content.Load<T>(path));
			}
			return (T)rscs[typeof(T)][path];
		}
	}
}
