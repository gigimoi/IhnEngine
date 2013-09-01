//
// ComponentOnClickedDo.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;

namespace IhnLib {
	[Serializable]
	public class ComponentOnClickedDo : Component{
		public EventHandler Todo;
		public ComponentOnClickedDo(EventHandler todo) {
			Todo = todo;
		}
	}
}

