//
// Entity.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using System.Collections.Generic;

namespace IhnEngine {
	public class Entity {
		public List<Component> Components = new List<Component>();
		public T GetComponent<T>() where T : Component{
			for(int i = 0; i < Components.Count; i++) {
				if(Components[i].GetType() == typeof(T)) {
					return Components[i] as T;
				}
			}
			return default(T);
		}
	}
}

