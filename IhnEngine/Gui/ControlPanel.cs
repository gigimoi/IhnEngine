using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
    /// <summary>
    /// Control that uses a 96x96 image to render a panel
    /// </summary>
	public class ControlPanel : Control{
        /// <summary>
        /// Path to image that is slived and rendered
        /// </summary>
		public string Img;
        /// <summary>
        /// Constructs a new control panel with a parent, size, and image path
        /// </summary>
        /// <param name="parent">Control to be a child of</param>
        /// <param name="size">Width + Height of this panel</param>
        /// <param name="img">Path to image</param>
        public ControlPanel(Control parent, Vector2 size, string img) : base(parent, size) { Img = img; }
        /// <summary>
        /// Renders the panel
        /// </summary>
        /// <param name="ihn">Ihn to render with</param>
        /// <param name="spriteBatch">Spritebatch to render with</param>
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
