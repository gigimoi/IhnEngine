using IhnLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
    /// <summary>
    /// Component to indicate that an item can be placed into a slot
    /// </summary>
	[Serializable]
	public class ComponentSlotted : Component{
        /// <summary>
        /// List of strings that this component's entity can fit into
        /// </summary>
		public List<string> Slots = new List<string>();
        /// <summary>
        /// Creates a new component slotted that can fit into slot
        /// </summary>
        /// <param name="slot">Slot this component can fit into</param>
		public ComponentSlotted(string slot) {
			Slots.Add(slot);
		}
        /// <summary>
        /// Creates a new component slotted that can fit into slots
        /// </summary>
        /// <param name="slots">Slots this component can fit into</param>
		public ComponentSlotted(List<string> slots) {
			Slots = slots;
		}
	}
}
