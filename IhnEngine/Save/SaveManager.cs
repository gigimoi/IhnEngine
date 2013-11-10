using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

namespace IhnLib {
    /// <summary>
    ///  Serialization helper to simplify things.
    /// </summary>
	public static class SaveManager {
        /// <summary>
        /// Serializes an object and saves it to a path. Simplest savefile possible
        /// </summary>
        /// <param name="obj">Object to save</param>
        /// <param name="saveFile">Path to save to</param>
        public static void SaveObject(Object obj, string saveFile) {
            BinaryFormatter bin = new BinaryFormatter();
            var stream = new FileStream(saveFile, FileMode.OpenOrCreate);
            bin.Serialize(stream, obj);
            stream.Close();
        }
        /// <summary>
        /// Deserializes an object at the path. Simplest savefile possible
        /// </summary>
        /// <param name="file">Object to load</param>
        /// <returns>Loaded object</returns>
        public static Object LoadObject(string file) {
            Stream stream = File.Open(file, FileMode.Open);
            BinaryFormatter bin = new BinaryFormatter();
            var fin = bin.Deserialize(stream);
            stream.Close();
            return fin;
        }
        /// <summary>
        /// Saves an entire Ihn, all of the systems and entities anyways
        /// </summary>
        /// <param name="ihn">Ihn to save</param>
        /// <param name="saveFile">Path saved to</param>
		public static void Save(Ihn ihn, string saveFile) {
			List<string> dat = new List<string>();
			File.Delete(saveFile);
			Stream stream = File.Open(saveFile, FileMode.Create);
			for(int i = 0; i < ihn.EntityCount; i++) {
				var ent = ihn.GetEntityAt(i);
				BinaryFormatter bin = new BinaryFormatter();
				bin.Serialize(stream, ent);
			}
			for(int i = 0; i < ihn.SystemCount; i++) {
				var sys = ihn.GetSystemAt(i);
				BinaryFormatter bin = new BinaryFormatter();
				bin.Serialize(stream, sys);
			}
			stream.Close();
		}
        /// <summary>
        /// Loads a whole Ihn, all of the entities and systems
        /// </summary>
        /// <param name="ihn">Ihn to load over</param>
        /// <param name="saveFile">Path to file to load from</param>
		public static void Load(Ihn ihn, string saveFile) {
			Stream stream = File.Open(saveFile, FileMode.Open);
			BinaryFormatter bin = new BinaryFormatter();
			ihn.ClearEntities();
			ihn.ClearSystems();
			while(true) {
				try {
					var a = bin.Deserialize(stream);
					if(a.GetType() == typeof(Entity)) {
						dynamic b = a;
						ihn.AddEntity(b);
					}
					else {
						dynamic b = a;
						ihn.AddSystem(b);
					}
				} catch {
					break;
				}
			}
			stream.Close();
		}
	}
}