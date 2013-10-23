using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
	public class ControlPanel : Control{
		public string Img;
        public ControlPanel(Control parent, Vector2 size, string img) : base(parent, size) { Img = img; }
		public override void Render(Ihn ihn, SpriteBatch spriteBatch) {
			for(int i = (int)Root.X + 32; i < Size.X + Root.X - 32; i += 32) {
				for(int j = (int)Root.Y + 32; j < Size.Y + Root.Y - 32; j += 32) {
					spriteBatch.Draw(Rsc.Load<Texture2D>(Img), new FloatRect(i, j, 32, 32).ToRect(),
						new Rectangle(32, 32, 32, 32), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
				}
			}
			for(int i = (int)Root.X + 32; i < Size.X + Root.X - 32; i += 32) {
				spriteBatch.Draw(Rsc.Load<Texture2D>(Img), new FloatRect(i, Root.Y, 32, 32).ToRect(),
					new Rectangle(32, 0, 32, 32), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
				spriteBatch.Draw(Rsc.Load<Texture2D>(Img), new FloatRect(i, Root.Y + Size.Y - 32, 32, 32).ToRect(),
					new Rectangle(32, 64, 32, 32), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
			}
			for(int i = (int)Root.Y + 32; i < Size.Y + Root.Y - 32; i += 32) {
				spriteBatch.Draw(Rsc.Load<Texture2D>(Img), new FloatRect(Root.X, i, 32, 32).ToRect(),
					new Rectangle(0, 32, 32, 32), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
				spriteBatch.Draw(Rsc.Load<Texture2D>(Img), new FloatRect(Root.X + Size.X - 32, i, 32, 32).ToRect(),
					new Rectangle(64, 32, 32, 32), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
			}
			spriteBatch.Draw(Rsc.Load<Texture2D>(Img), new FloatRect(Root.X, Root.Y, 32, 32).ToRect(),
				new Rectangle(0, 0, 32, 32), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
			spriteBatch.Draw(Rsc.Load<Texture2D>(Img), new FloatRect(Root.X + Size.X - 32, Root.Y, 32, 32).ToRect(),
				new Rectangle(64, 0, 32, 32), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
			spriteBatch.Draw(Rsc.Load<Texture2D>(Img), new FloatRect(Root.X, Root.Y + Size.Y - 32, 32, 32).ToRect(),
				new Rectangle(0, 64, 32, 32), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
			spriteBatch.Draw(Rsc.Load<Texture2D>(Img), new FloatRect(Root.X + Size.X - 32, Root.Y + Size.Y - 32, 32, 32).ToRect(),
				new Rectangle(64, 64, 32, 32), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
			base.Render(ihn, spriteBatch);
		}
	}
}
