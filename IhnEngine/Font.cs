using IhnLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Xml.Serialization;

namespace IhnLib {
    /// <summary>
    /// Manages font
    /// </summary>
	public static class Font {
        /// <summary>
        /// Loads font
        /// </summary>
		public static FontFile Load(string path) {
			var stream = new FileStream(path, FileMode.Open);
			XmlSerializer deserializer = new XmlSerializer(typeof(FontFile));
			FontFile file = (FontFile)deserializer.Deserialize(stream);
			return file;
		}
	}
    /// <summary>
    /// Replacement for spritefont which cannot be created from ttfs
    /// </summary>
	public class DrawableFont {
        /// <summary>
        /// Loaded by constructor, the inner file from BMFont
        /// </summary>
		FontFile Font;
        /// <summary>
        /// String reference to the graphic for the font
        /// </summary>
		public static string Img;
		private static FontRenderer fr;
        /// <summary>
        /// Creates a new font
        /// </summary>
        /// <param name="path">Path to the font</param>
		public DrawableFont(string path) {
			Font = IhnLib.Font.Load(path);
			Img = path.Substring(0, path.Length - 4);
            //Images sometimes have _1 at the end, increase the graphic size in this case
			Img += "_0.png";
			fr = new FontRenderer(Font, Rsc.Load<Texture2D>(Img));
		}
        /// <summary>
        /// Gets the rectangle around the text that would be drawn at size with orientation and text
        /// </summary>
        /// <param name="text">String that would be displayed</param>
        /// <param name="size">Size the string would be displayed at</param>
        /// <param name="orientation">Orientation the renderer would assume</param>
        /// <returns>Rectangle of what the text would cover</returns>
		public Rectangle RectangleOf(string text, int size, TextOrientation orientation = TextOrientation.Left) {
			return fr.RectangleOf(text, size, orientation);
		}
        
        /// <summary>
        /// Renders text
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to render with</param>
        /// <param name="x">X Coord to render at</param>
        /// <param name="y">Y Coord to render at</param>
        /// <param name="text">String to render</param>
        /// <param name="color">Color to render</param>
        /// <param name="size">Size to render at</param>
        /// <param name="orientation">Orientation to render at</param>
		public void Draw(SpriteBatch spriteBatch, int x, int y, string text, Color color, float size = 16, TextOrientation orientation = TextOrientation.Left) {
			fr.DrawText(spriteBatch, x, y, text, color, size, orientation);
		}
        /// <summary>
        /// Renders text with an outline
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to render with</param>
        /// <param name="x">X Coord to render at</param>
        /// <param name="y">Y Coord to render at</param>
        /// <param name="text">String to render</param>
        /// <param name="inside">Color within the text</param>
        /// <param name="outside">Color of the outline</param>
        /// <param name="size">Size to render at</param>
        /// <param name="orientation">Orientation to render at</param>
		public void Draw(SpriteBatch spriteBatch, int x, int y, string text, Color inside, Color outside, float size = 16, TextOrientation orientation = TextOrientation.Left) {
			Draw(spriteBatch, x + 1, y + 1, text, outside, size, orientation);
			Draw(spriteBatch, x + 1, y - 1, text, outside, size, orientation);
			Draw(spriteBatch, x - 1, y + 1, text, outside, size, orientation);
			Draw(spriteBatch, x - 1, y - 1, text, outside, size, orientation);
			Draw(spriteBatch, x, y, text, inside, size, orientation);
		}
	}
}