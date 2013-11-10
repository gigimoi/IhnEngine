using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace IhnLib {
    /// <summary>
    /// Core of the entity component system, essentially a list of components
    /// </summary>
	[Serializable]
	public class Entity {
        /// <summary>
        /// Dictionary of all components within this entity, accessed by type
        /// </summary>
		public Dictionary<Type, Component> Components = new Dictionary<Type, Component>();
        /// <summary>
        /// Adds a component to this entity and registers that it is owned to the ihn
        /// </summary>
        /// <param name="component">Component to add</param>
		public void AddComp(Component component) {
			Components.Add(component.GetType(), component);
			Ihn.Instance.RegisterEntityHasComponent(component.GetType(), this);
		}
        /// <summary>
        /// Gets the component from this entity of type T
        /// </summary>
        /// <typeparam name="T">Component type to get</typeparam>
        /// <returns>Component of Type T</returns>
		public T GetComp<T>() where T : Component {
			if(Components.ContainsKey(typeof(T))) {
				return (T)Components[typeof(T)];
			}
			return null;
		}
        /// <summary>
        /// Gets component by index rather than by type, not recommended
        /// </summary>
        /// <param name="index">Index to access</param>
        /// <returns>Component at index</returns>
		public Component GetComp(int index) {
			var comps = new Component[Components.Count];
			Components.Values.CopyTo(comps, 0);
			if(index < comps.Length) {
				return comps[index];
			}
			return null;
		}
        /// <summary>
        /// Checks if the entity contains a component of type T
        /// </summary>
        /// <typeparam name="T">Type to check</typeparam>
        /// <returns>True if it has the component T</returns>
		public bool HasComp<T>() where T : Component {
			return Components.ContainsKey(typeof(T));
		}
        /// <summary>
        /// Checks if the entity contains a component of type T
        /// </summary>
        /// <param name="T">Type to check</param>
        /// <returns>True if it has the component T</returns>
		public bool HasComp(Type T) {
			return(Components.ContainsKey(T));
		}
        /// <summary>
        /// Removes component from entity and registers that it has been removed to the ihn
        /// </summary>
        /// <typeparam name="T">Component type to remove</typeparam>
		public void RemoveComp<T>() where T : Component {
			Ihn.Instance.UnRegisterEntityHasComponent(typeof(T), this);
			if(Components.ContainsKey(typeof(T))) {
				Components.Remove(typeof(T));
			}
		}
        /// <summary>
        /// Clones the entity by serialization
        /// </summary>
        /// <returns>Cloned entity</returns>
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

