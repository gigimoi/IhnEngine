using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
    public class ControlLabel : Control {
        public DrawableFont Font;
        public string Text;
        public Color Col;
        public int TextSize;
        public ControlLabel(Control parent, string text, DrawableFont font, Color color, int size = 16)
            : base(parent, new Vector2(0, 0)) {
                Font = font;
                Text = text;
                Col = color;
                TextSize = size;
        }
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
