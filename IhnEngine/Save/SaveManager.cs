using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

namespace IhnLib {
	public static class SaveManager {
		public static string Root = "saves";
        public static void SaveObject(Object obj, string saveFile) {
            BinaryFormatter bin = new BinaryFormatter();
            var stream = new FileStream(saveFile, FileMode.OpenOrCreate);
            bin.Serialize(stream, obj);
            stream.Close();
        }
        public static Object LoadObject(string file) {
            Stream stream = File.Open(file, FileMode.Open);
            BinaryFormatter bin = new BinaryFormatter();
            var fin = bin.Deserialize(stream);
            stream.Close();
            return fin;
        }
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