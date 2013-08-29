//
// SaveManager.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace IhnLib {
	public static class SaveManager {
		public static string Root = "saves";
		public static void Save(Ihn ihn, string saveFile) {
			List<string> dat = new List<string>();
			Stream stream = File.Open(saveFile, FileMode.OpenOrCreate);
			for(int i = 0; i < ihn.EntityCount; i++) {
				var ent = ihn.GetEntityAt(i);
				BinaryFormatter bin = new BinaryFormatter();
				bin.Serialize(stream, ent);
			}
		}
	}
}