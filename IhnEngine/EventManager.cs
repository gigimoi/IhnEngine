//
// EventManager.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using System.Collections.Generic;

namespace IhnLib {
	public static class EventManager {
		static Dictionary<string, EventHandler> _events = new Dictionary<string, EventHandler>();
		public static void Listen(string str, EventHandler method) {
			if(!_events.ContainsKey(str)) {
				_events.Add(str, null);
			}
			_events[str] = _events[str] + method;
		}
		public static void UnListen(string str, EventHandler method) {
			if(!_events.ContainsKey(str)) {
				_events.Add(str, null);
			}
			_events[str] = _events[str] - method;
		}
		public static void Raise(string str, object sender, EventArgs e) {
			if(!_events.ContainsKey(str)) {
				_events.Add(str, null);
			}
			if(_events[str] != null) {
				_events[str].Invoke(sender, e);
			}
		}
		public static void Raise(string str) {
			Raise(str, new object(), new EventArgs());
		}
	}
}

