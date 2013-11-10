using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
    /// <summary>
    /// GUI Control to render a sprite
    /// </summary>
	public class ControlSprite : Control{
		string _img;
        /// <summary>
        /// Path to the sprite, will override size if changed
        /// </summary>
		public string Img {
			get {
				return _img;
			}
			set {
				_img = value;
				Size = new Vector2(Rsc.Load<Texture2D>(_img).Width, Rsc.Load<Texture2D>(_img).Height);
			}
		}
        /// <summary>
        /// Should this sprite be mirrored in any direction
        /// </summary>
		public SpriteEffects Mirror = SpriteEffects.None;
        /// <summary>
        /// Instantiates a new ControlSprite with parent and a string reference to an image
        /// </summary>
        /// <param name="parent">Control to be a child of</param>
        /// <param name="img">Path to image</param>
		public ControlSprite(Control parent, string img)
			: base(parent, new Vector2(Rsc.Load<Texture2D>(img).Width, Rsc.Load<Texture2D>(img).Height)) {
			Img = img;
		}
        /// <summary>
        /// Renders the sprite
        /// </summary>
        /// <param name="ihn">Ihn the control is contained in</param>
        /// <param name="spriteBatch">Spritebatch to draw with</param>
		public override void Render(Ihn ihn, SpriteBatch spriteBatch) {
			spriteBatch.Draw(Rsc.Load<Texture2D>(Img), 
				new FloatRect(Root.X, Root.Y, Size.X, Size.Y).ToRect(), 
				new Rectangle(0, 0, (int)Size.X, (int)Size.Y), 
				Color.White, 0, new Vector2(0, 0), Mirror, 0);
			base.Render(ihn, spriteBatch);
		}
	}
}
