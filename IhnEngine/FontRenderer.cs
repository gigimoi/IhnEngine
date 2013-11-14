using IhnLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace IhnLib {
    /// <summary>
    /// Extern
    /// </summary>
    class FontRenderer {
        /// <summary>
        /// Extern
        /// </summary>
        /// <param name="fontFile"></param>
        /// <param name="fontTexture"></param>
		public FontRenderer(FontFile fontFile, Texture2D fontTexture) {
			_fontFile = fontFile;
			_texture = fontTexture;
			_characterMap = new Dictionary<char, FontChar>();

			foreach(var fontCharacter in _fontFile.Chars) {
				char c = (char)fontCharacter.ID;
				try {
					_characterMap.Add(c, fontCharacter);
				}
				catch { };
			}
		}

		private Dictionary<char, FontChar> _characterMap;
		private FontFile _fontFile;
		private Texture2D _texture;
        /// <summary>
        /// Extern
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="scale"></param>
        /// <param name="orientation"></param>
		public void DrawText(SpriteBatch spriteBatch, int x, int y, string text, Color color, float scale, TextOrientation  orientation = TextOrientation.Left) {
			int dx = x;
			int dy = y;
			if(orientation != TextOrientation.Left) {
				foreach(char c in text) {
					FontChar fc;
					if(_characterMap.TryGetValue(c, out fc)) {
						var wy = fc.Height / 64f * scale;
						var wx = wy / fc.Height * fc.Width;
						dx -= (int)(((int)wx + 4) * (orientation == TextOrientation.Center ? 0.5f : 1f));
					}
				}
			}
			foreach(char c in text) {
				FontChar fc;
				if(_characterMap.TryGetValue(c, out fc)) {
					var sourceRectangle = new Rectangle(fc.X, fc.Y, fc.Width, fc.Height);
					var position = new Vector2(dx + (fc.XOffset / 64f * scale) / fc.Height * fc.Width, dy + fc.YOffset / 64f * scale);
					var wy = fc.Height / 64f * scale;
					var wx = wy / fc.Height * fc.Width;
					spriteBatch.Draw(_texture, 
									 new Rectangle((int)position.X, (int)position.Y,
												   (int)(wx), (int)(wy)),
									 sourceRectangle, color);
					dx += (int)wx + 4;
				}
			}
		}
        /// <summary>
        /// Extern
        /// </summary>
        /// <param name="text"></param>
        /// <param name="scale"></param>
        /// <param name="orientation"></param>
        /// <returns></returns>
		public Rectangle RectangleOf(string text, int scale, TextOrientation orientation = TextOrientation.Left) {
			int dx = 0;
			int dy = 0;
			int bigWidth = 0;
            int currentWidth = 0;
			int height = 0;
            int i = 0;
			if(orientation != TextOrientation.Left) {
				foreach(char c in text) {
                    i++;
					FontChar fc;
					if(_characterMap.TryGetValue(c, out fc)) {
						var wy = fc.Height / 64f * scale;
						var wx = wy / fc.Height * fc.Width;
						dx -= (int)(((int)wx + 4) * (orientation == TextOrientation.Center ? 0.5f : 1f));
                        currentWidth += (int)wx + 4;
                        bigWidth = Math.Max(bigWidth, currentWidth);
						height = Math.Max(height, (int)(fc.Height / 64f * scale) + 2);
                        if (text[i] == "\n"[0]) {
                            currentWidth = 0;
                        }
					}
				}
			}
			else {
				foreach(char c in text) {
					FontChar fc;
					if(_characterMap.TryGetValue(c, out fc)) {
						var wy = fc.Height / 64f * scale;
						var wx = wy / fc.Height * fc.Width;
						bigWidth += (int)wx + 4;
						height = Math.Max(height, (int)(fc.Height / 64f * scale) + 2);
					}
				}
			}
			return new Rectangle(dx, dy, bigWidth, height);
		}
	}
    /// <summary>
    /// Chooses how the text is oriented, similar to microsoft word
    /// </summary>
	public enum TextOrientation {
        /// <summary>
        /// Root of the text at the left
        /// </summary>
		Left,
        /// <summary>
        /// Root of text at the center
        /// </summary>
		Center,
        /// <summary>
        /// Root of text at the right
        /// </summary>
		Right
	}
}