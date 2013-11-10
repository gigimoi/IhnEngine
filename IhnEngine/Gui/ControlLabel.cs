using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
    /// <summary>
    /// GUI Control to render text
    /// </summary>
    public class ControlLabel : Control {
        /// <summary>
        /// Font to render with
        /// </summary>
        public DrawableFont Font;
        /// <summary>
        /// Text to render
        /// </summary>
        public string Text;
        /// <summary>
        /// Color of text
        /// </summary>
        public Color Col;
        /// <summary>
        /// Size of text
        /// </summary>
        public int TextSize;
        /// <summary>
        /// Instantiates a new control panel
        /// </summary>
        /// <param name="parent">Control to become a child of</param>
        /// <param name="text">Text value of the control</param>
        /// <param name="font">Font to render with</param>
        /// <param name="color">Color of the rendered text</param>
        /// <param name="textSize">Size of the text</param>
        public ControlLabel(Control parent, string text, DrawableFont font, Color color, int textSize = 16)
            : base(parent, new Vector2(0, 0)) {
                Font = font;
                Text = text;
                Col = color;
                TextSize = textSize;
        }
        /// <summary>
        /// Updates the size of this component
        /// </summary>
        /// <param name="ihn">Ihn calling this function</param>
        public override void Update(Ihn ihn) {
            base.Update(ihn);
            List<string> toRender = new List<string>();
            var newText = Text;
            while (newText.Contains("\n")) {
                toRender.Add(newText.Substring(0, newText.IndexOf("\n")));
                newText = newText.Substring(newText.IndexOf("\n") + 1);
            }
            toRender.Add(newText);
            int _y = 0;
            for (int i = 0; i < toRender.Count; i++) {
                _y += TextSize;
            }
            this.Size.Y = _y;
        }//TODO: Store toRender
        /// <summary>
        /// Renders the text
        /// </summary>
        /// <param name="ihn">Ihn calling this function</param>
        /// <param name="spriteBatch">Spritebatch to render with</param>
        public override void Render(Ihn ihn, SpriteBatch spriteBatch) {
            List<string> toRender = new List<string>();
            var newText = Text;
            while (newText.Contains("\n")) {
                toRender.Add(newText.Substring(0, newText.IndexOf("\n")));
                newText = newText.Substring(newText.IndexOf("\n") + 1);
            }
            toRender.Add(newText);
            int _y = 0;
            for (int i = 0; i < toRender.Count; i++) {
                Font.Draw(spriteBatch, (int)Root.X, (int)Root.Y + _y, toRender[i], Col, TextSize);
                _y += TextSize;
            }
            this.Size.Y = _y;
            base.Render(ihn, spriteBatch);
        }
    }
}
