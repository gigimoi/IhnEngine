//
using System;

namespace IhnLib {
    /// <summary>
    /// Component that stores an event that is run when an entity is left clicked
    /// </summary>
	[Serializable]
	public class ComponentOnClickedDo : Component{
        /// <summary>
        /// Event to run
        /// </summary>
		public EventHandler Todo;
        /// <summary>
        /// Instantiates ComponentOnClickedDo
        /// </summary>
        /// <param name="todo">Event to run when clicked</param>
		public ComponentOnClickedDo(EventHandler todo) {
			Todo = todo;
		}
	}
}

