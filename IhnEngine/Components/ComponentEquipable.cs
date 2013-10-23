using IhnLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
	[Serializable]
	public class ComponentEquipable : Component{
		public List<string> Slots = new List<string>();
		public ComponentEquipable(string slot) {
			Slots.Add(slot);
		}
		public ComponentEquipable(List<string> slots) {
			Slots = slots;
		}
	}
}
