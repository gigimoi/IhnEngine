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
			if(path != "") {
				if(!rscs[typeof(T)].ContainsKey(path)) {
					var dat = Ihn.Instance.Content.Load<T>(path);
					if(dat.GetType() == typeof(Texture2D)) {
						(dat as Texture2D).Name = path;
					}
					rscs[typeof(T)].Add(path, dat);
				}
				return (T)rscs[typeof(T)][path];
			}
			if(typeof(T) == typeof(Texture2D)) {
				if(!rscs[typeof(T)].ContainsKey("NULL_DATA")) {
					rscs[typeof(T)].Add("NULL_DATA", Art.GetNone());
				}
				return (T)rscs[typeof(T)]["NULL_DATA"];
			}
			return default(T);
		}
	}
}