using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IhnLib {
    /// <summary>
    /// Allows an entity to be rendered in a GUI setting
    /// </summary>
	[Serializable]
	public class ComponentInventorySprite : Component{
        /// <summary>
        /// Delegate function for overriding the inventory sprite render
        /// </summary>
        /// <param name="ihn">Ihn to use</param>
        /// <param name="entity">Entity rendered</param>
        /// <param name="spriteBatch">Spritebatch used</param>
        /// <param name="x">XCoord</param>
        /// <param name="y">YCoord</param>
        /// <param name="mirrorX">Mirror along the y axis</param>
		public delegate void InventorySpriteRenderDelegate(Ihn ihn, Entity entity, SpriteBatch spriteBatch, int x, int y, bool mirrorX = false);
		/// <summary>
		/// Default texture
		/// </summary>
        public string Texture;
        /// <summary>
        /// InventorySpriteRenderDelegate to render with, defaults to ComponentInventorySprite.Render
        /// </summary>
		public InventorySpriteRenderDelegate Renderer = Render;
        /// <summary>
        /// Instantiates component
        /// </summary>
        /// <param name="sprite">Default rendereer sprite</param>
		public ComponentInventorySprite(string sprite) {
			Texture = sprite;
		}
        /// <summary>
        /// Default render function
        /// </summary>
        /// <param name="ihn">Ihn instance to use</param>
        /// <param name="entity">Entity rendered</param>
        /// <param name="spriteBatch">Spritebatch to render with</param>
        /// <param name="x">XCoord</param>
        /// <param name="y">YCoord</param>
        /// <param name="mirrorx">Mirror on Y axis?</param>
		public static void Render(Ihn ihn, Entity entity, SpriteBatch spriteBatch, int x, int y, bool mirrorx = false) {
			var sprite = entity.GetComp<ComponentInventorySprite>();
            spriteBatch.Draw(Rsc.Load<Texture2D>(sprite.Texture), new Rectangle(x, y, Rsc.Load<Texture2D>(sprite.Texture).Width, Rsc.Load<Texture2D>(sprite.Texture).Height), null, Color.White, 0f, new Vector2(0, 0), mirrorx ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
		}
	}
}

