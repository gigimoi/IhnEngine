using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace IhnLib {
	public static class Art {
		static Texture2D _pixel;
		static Texture2D _none;
		public static Texture2D GetPixel() {
			if(_pixel == null) {
				_pixel = new Texture2D(Ihn.Instance.GraphicsDevice, 1, 1);
				_pixel.SetData<Color>(new Color[] { Color.White });
			}
			return _pixel;
		}
		public static Texture2D GetNone() {
			if(_none == null) {
				_none = new Texture2D(Ihn.Instance.GraphicsDevice, 1, 1);
				_none.SetData<Color>(new Color[] { new Color(0, 0, 0, 0) });
			}
			return _none;
		}
		public static Color[,] ToColArray(this Texture2D self) {
			var data = new Color[self.Width * self.Height];
			var fin = new Color[self.Width, self.Height];
			self.GetData<Color>(data);
			for(int i = 0; i < self.Width; i++) {
				for(int j = 0; j < self.Height; j++) {
					fin[i, j] = data[i + j * self.Height];
				}
			}
			return fin;
		}
		public static Texture2D ToTexture2D(this Color[,] self) {
			var fin = new Texture2D(Ihn.Instance.GraphicsDevice, self.GetLength(0), self.GetLength(1));
			var data = new Color[self.GetLength(0) * self.GetLength(1)];
			for(int i = 0; i < self.GetLength(0); i++) {
				for(int j = 0; j < self.GetLength(1); j++) {
					data[i + j * self.GetLength(0)] = self[i, j];
				}
			}
			fin.SetData<Color>(data);
			return fin;
		}
		public static Texture2D FillRectangle(this Texture2D self, Rectangle area, Color color) {
			return FillRectangles(self, new List<Rectangle> { area }, color);
		}
		public static Texture2D FillRectangles(this Texture2D self, List<Rectangle> areas, Color color) {
			var col = self.ToColArray();
			for(int k = 0; k < areas.Count; k++) {
				var area = areas[k];
				for(int i = area.X; i < Math.Max(0, Math.Min(area.X + area.Width, self.Width)); i++) {
					for(int j = area.Y; j < Math.Max(0, Math.Min(area.Y + area.Height, self.Height)); j++) {
						col[i, j] = color;
					}
				}
			}
			return col.ToTexture2D();
		}
	}
}

