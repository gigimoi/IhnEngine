using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
	public class ControlSprite : Control{
		string _img;
		public string Img {
			get {
				return _img;
			}
			set {
				_img = value;
				Size = new Vector2(Rsc.Load<Texture2D>(_img).Width, Rsc.Load<Texture2D>(_img).Height);
			}
		}
		public SpriteEffects Mirror = SpriteEffects.None;
		public ControlSprite(Control parent, string img)
			: base(parent, new Vector2(Rsc.Load<Texture2D>(img).Width, Rsc.Load<Texture2D>(img).Height)) {
			Img = img;
		}
		public override void Render(Ihn ihn, SpriteBatch spriteBatch) {
			spriteBatch.Draw(Rsc.Load<Texture2D>(Img), 
				new FloatRect(Root.X, Root.Y, Size.X, Size.Y).ToRect(), 
				new Rectangle(0, 0, (int)Size.X, (int)Size.Y), 
				Color.White, 0, new Vector2(0, 0), Mirror, 0);
			base.Render(ihn, spriteBatch);
		}
	}
}
