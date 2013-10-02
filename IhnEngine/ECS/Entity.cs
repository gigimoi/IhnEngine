//
// Entity.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace IhnLib {
	[Serializable]
	public class Entity {
		public Dictionary<Type, Component> Components = new Dictionary<Type, Component>();
		public void AddComp(Component component) {
			Components.Add(component.GetType(), component);
			Ihn.Instance.RegisterEntityHasComponent(component.GetType(), this);
		}
		public T GetComp<T>() where T : Component {
			if(Components.ContainsKey(typeof(T))) {
				return (T)Components[typeof(T)];
			}
			return null;
		}
		public Component GetComp(int index) {
			var comps = new Component[Components.Count];
			Components.Values.CopyTo(comps, 0);
			if(index < comps.Length) {
				return comps[index];
			}
			return null;
		}
		public bool HasComp<T>() where T : Component {
			return Components.ContainsKey(typeof(T));
		}
		public bool HasComp(Type T) {
			return(Components.ContainsKey(T));
		}
		public void RemoveComp<T>() where T : Component {
			Ihn.Instance.UnRegisterEntityHasComponent(typeof(T), this);
			if(Components.ContainsKey(typeof(T))) {
				Components.Remove(typeof(T));
			}
		}
		public Entity Clone() {
			Stream saveStream = new FileStream("ASD.ASD", FileMode.Create);
			BinaryFormatter bin = new BinaryFormatter();
			bin.Serialize(saveStream, this);
			saveStream.Close();

			Stream loadStream = new FileStream("ASD.ASD", FileMode.Open);
			bin = new BinaryFormatter();
			var ent = (Entity)bin.Deserialize(loadStream);
			loadStream.Close();

			File.Delete("ASD.ASD");

			return ent;

		}
	}
}

