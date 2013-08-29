//
// SaveManager.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System.Collections.Generic;
using System;
using System.Reflection;

namespace IhnLib {
	public static class SaveManager {
		public static string Root = "saves";
		public static void Save(Ihn ihn, string saveFile) {
			List<string> dat = new List<string>();
			for(int i = 0; i < ihn.EntityCount; i++) {
				var ent = ihn.GetEntityAt(i);
				dat.Add("~!:!~");
				for(int j = 0; j < ent.Components.Count; j++) {
					var comp = ent.GetComp(j);
					var fields = ((FieldInfo[])comp.GetType().GetRuntimeFields());
					for(int k = 0; k < fields.Length; k++) {
						dat.Add(comp.GetType().Name + "~~:~~" + fields[k].Name + "~@:@~" + fields[k].GetValue(comp));
					}
				}
			}
			for(int i = 0; i < dat.Count; i++) {
				Console.WriteLine(dat[i]);
			}
		}
	}
}