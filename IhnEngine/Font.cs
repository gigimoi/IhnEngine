using BmFont;
using IhnLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Xml.Serialization;

namespace IhnLib {
	static class Font {
		public static FontFile Load(string path) {
			var stream = new FileStream(path, FileMode.Open);
			XmlSerializer deserializer = new XmlSerializer(typeof(FontFile));
			FontFile file = (FontFile)deserializer.Deserialize(stream);
			return file;
		}
	}
	public class DrawableFont {
		public FontFile Font;
		public static string Img;
		private static FontRenderer fr;
		public DrawableFont(string path) {
			Font = IhnLib.Font.Load(path);
			Img = path.Substring(0, path.Length - 4);
			Img += "_0.png";
			fr = new FontRenderer(Font, Rsc.Load<Texture2D>(Img));
		}
		public Rectangle RectangleOf(string text, int size, TextOrientation orientation = TextOrientation.Left) {
			return fr.RectangleOf(text, size, orientation);
		}
		public void Draw(SpriteBatch spriteBatch, int x, int y, string text, Color color, float size = 16, TextOrientation orientation = TextOrientation.Left) {
			fr.DrawText(spriteBatch, x, y, text, color, size, orientation);
		}
		public void Draw(SpriteBatch spriteBatch, int x, int y, string text, Color inside, Color outside, float size = 16, TextOrientation orientation = TextOrientation.Left) {
			Draw(spriteBatch, x + 1, y + 1, text, outside, size, orientation);
			Draw(spriteBatch, x + 1, y - 1, text, outside, size, orientation);
			Draw(spriteBatch, x - 1, y + 1, text, outside, size, orientation);
			Draw(spriteBatch, x - 1, y - 1, text, outside, size, orientation);
			Draw(spriteBatch, x, y, text, inside, size, orientation);
		}
	}
}