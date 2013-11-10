using System;
using System.Drawing;

namespace IhnLib {
    /// <summary>
    /// Attaches a string and a font to an entity
    /// </summary>
	[Serializable]
	public class ComponentString : Component{
        /// <summary>
        /// Text attached
        /// </summary>
		public string Text;
        /// <summary>
        /// Font to render Text with
        /// </summary>
		public DrawableFont Font;

        /// <summary>
        /// Instantiates a new ComponentString
        /// </summary>
        /// <param name="text">Initial text</param>
        /// <param name="font">Font to render with</param>
		public ComponentString(string text, DrawableFont font) {
			Font = font;
			Text = text;
		}
	}
}

