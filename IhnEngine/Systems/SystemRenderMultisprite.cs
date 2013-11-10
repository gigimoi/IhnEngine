using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IhnLib {
    /// <summary>
    /// Renders multiple sprites at once
    /// </summary>
	[Serializable]
	public class SystemRenderMultiSprite : ISystem {
        /// <summary>
        /// This system does not update
        /// </summary>
        /// <param name="ihn">Ihn entity is contained in</param>
        /// <param name="entity">Entity to update</param>
		public void Update(Ihn ihn, Entity entity) { }
        /// <summary>
        /// Renders the sprites
        /// </summary>
        /// <param name="ihn">Ihn entity is contained in</param>
        /// <param name="spriteBatch">Spritebatch to draw with</param>
        /// <param name="entity">Entity to draw</param>
		public void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity) {
			var pos = entity.GetComp<ComponentPosition>();
			var sprite = entity.GetComp<ComponentMultiSprite>();
			for(int i = 0; i < sprite.Textures.Count; i++) {
				if(sprite.Textures[i] != null) {
					if(entity.HasComp<ComponentSize>()) {
						var size = entity.GetComp<ComponentSize>();
						spriteBatch.Draw(Rsc.Load<Texture2D>(sprite.Textures[i]), new Rectangle((int)pos.X - (int)ihn.CameraPos.X, (int)pos.Y - (int)ihn.CameraPos.Y, size.Width, size.Height), sprite.Source.ToRect(), Color.White, MathHelper.ToRadians(sprite.Rotation), sprite.Origin.ToVector(), sprite.Mirror, 0);
					}
					else {
						spriteBatch.Draw(Rsc.Load<Texture2D>(sprite.Textures[i]), new Rectangle((int)pos.X - (int)ihn.CameraPos.X, (int)pos.Y - (int)ihn.CameraPos.Y, (int)sprite.Source.Width, (int)sprite.Source.Height), sprite.Source.ToRect(), Color.White, MathHelper.ToRadians(sprite.Rotation), sprite.Origin.ToVector(), sprite.Mirror, 0);
					}
				}
			}
		}
        /// <summary>
        /// List of components required for this system to act on an entity. ComponentPosition and ComponentMultisprite
        /// </summary>
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() { 
					typeof(ComponentPosition), 
					typeof(ComponentMultiSprite)
				};
			}
		}
	}
}

