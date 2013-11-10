using System;
using System.Collections.Generic;

namespace IhnLib {
    /// <summary>
    /// Listens and unlistens to events.
    /// Warning: This will prevent GC of an object with an event listened to.
    /// </summary>
	public static class EventManager {
		static Dictionary<string, EventHandler> _events = new Dictionary<string, EventHandler>();
        /// <summary>
        /// Method will be triggered when str is raised
        /// </summary>
        /// <param name="str">String to listen for</param>
        /// <param name="method">Method to run when heard</param>
		public static void Listen(string str, EventHandler method) {
			if(!_events.ContainsKey(str)) {
				_events.Add(str, null);
			}
			_events[str] = _events[str] + method;
		}
        /// <summary>
        /// Method will no longer be triggered when str is raised
        /// </summary>
        /// <param name="str">String to unlisten</param>
        /// <param name="method">Method to unlisten</param>
		public static void UnListen(string str, EventHandler method) {
			if(!_events.ContainsKey(str)) {
				_events.Add(str, null);
			}
			_events[str] = _events[str] - method;
		}
        /// <summary>
        /// Triggers all events listening to the str, can also pass a sender and event args
        /// </summary>
        /// <param name="str">String to raise</param>
        /// <param name="sender">Object to send</param>
        /// <param name="e">Other event args</param>
		public static void Raise(string str, object sender, EventArgs e) {
			if(!_events.ContainsKey(str)) {
				_events.Add(str, null);
			}
			if(_events[str] != null) {
				_events[str].Invoke(sender, e);
			}
		}
        /// <summary>
        /// No event args or object sender version of raise
        /// </summary>
        /// <param name="str">string to raise</param>
		public static void Raise(string str) {
			Raise(str, new object(), new EventArgs());
		}
	}
}

