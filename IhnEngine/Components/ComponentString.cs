//
// ComponentString.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using System.Drawing;

namespace IhnLib {
	[Serializable]
	public class ComponentString : Component{
		public string Text;
		public Font Font;

		public ComponentString(string text, Font font) {
			Font = font;
			Text = text;
		}
	}
}

