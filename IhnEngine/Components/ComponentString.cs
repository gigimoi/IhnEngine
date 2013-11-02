using System;
using System.Drawing;

namespace IhnLib {
	[Serializable]
	public class ComponentString : Component{
		public string Text;
		public DrawableFont Font;

		public ComponentString(string text, DrawableFont font) {
			Font = font;
			Text = text;
		}
	}
}

