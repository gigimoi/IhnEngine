using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IhnLib {
	[Serializable]
	public class ComponentInventorySprite : Component{
		public delegate void InventorySpriteRenderDelegate(Ihn ihn, Entity entity, SpriteBatch spriteBatch, int x, int y, bool mirrorX = false);
		public string Texture;
		public InventorySpriteRenderDelegate Renderer = Render;
		public ComponentInventorySprite(string sprite) {
			Texture = sprite;
		}
		public static void Render(Ihn ihn, Entity entity, SpriteBatch spriteBatch, int x, int y, bool mirrorx = false) {
			var sprite = entity.GetComp<ComponentInventorySprite>();
            spriteBatch.Draw(Rsc.Load<Texture2D>(sprite.Texture), new Rectangle(x, y, Rsc.Load<Texture2D>(sprite.Texture).Width, Rsc.Load<Texture2D>(sprite.Texture).Height), null, Color.White, 0f, new Vector2(0, 0), mirrorx ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
		}
	}
}

