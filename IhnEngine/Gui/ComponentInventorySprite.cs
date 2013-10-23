using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IhnLib {
	[Serializable]
	public class ComponentInventorySprite : Component{
		public delegate void InventorySpriteRenderDelegate(Ihn ihn, Entity entity, SpriteBatch spriteBatch, int x, int y);
		public string Texture;
		public InventorySpriteRenderDelegate Renderer = Render;
		public ComponentInventorySprite(string sprite) {
			Texture = sprite;
		}
		public static void Render(Ihn ihn, Entity entity, SpriteBatch spriteBatch, int x, int y) {
			var sprite = entity.GetComp<ComponentInventorySprite>();
			spriteBatch.Draw(Rsc.Load<Texture2D>(sprite.Texture), new Vector2(x, y), Color.White);
		}
	}
}

