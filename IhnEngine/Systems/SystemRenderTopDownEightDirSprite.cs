using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace IhnLib {
    /// <summary>
    /// Renders a top-down 8-directional shooter sprite
    /// </summary>
	[Serializable]
	public class SystemRenderTopDownEightDirSprite : ISystem{
        /// <summary>
        /// This system does not update
        /// </summary>
        /// <param name="ihn">Ihn entity is contained in</param>
        /// <param name="entity">Entity to update</param>
		public void Update(Ihn ihn, Entity entity) {
		}
        /// <summary>
        /// Renders the 8-directional sprite
        /// </summary>
        /// <param name="ihn">Ihn entity is contained in</param>
        /// <param name="spriteBatch">Spritebatch to draw with</param>
        /// <param name="entity">Entity to draw</param>
		public void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity) {
			var sprite = entity.GetComp<ComponentTopDownEightDirSprite>();
			var pos = entity.GetComp<ComponentPosition>();
			var dir = entity.GetComp<ComponentDirection>();
			int sizex = Rsc.Load<Texture2D>(sprite.Texture).Width / 4;
			int sizey = Rsc.Load<Texture2D>(sprite.Texture).Height / 2;
			if(entity.HasComp<ComponentSize>()) {
				var size = entity.GetComp<ComponentSize>();
				sizex = size.Width;
				sizey = size.Height;
			}
			float Rotation = DirectionHelper.ToAngle(dir.Dir);
			spriteBatch.Draw(Rsc.Load<Texture2D>(sprite.Texture), 
			                 new Rectangle((int)pos.X - (int)ihn.CameraPos.X, (int)pos.Y - (int)ihn.CameraPos.Y, sizex, sizey),
			                 new Rectangle(DirectionHelper.IsDiagonal(dir.Dir) ? Rsc.Load<Texture2D>(sprite.Texture).Width / 2 : 0, 0, Rsc.Load<Texture2D>(sprite.Texture).Width / 2, Rsc.Load<Texture2D>(sprite.Texture).Height), 
			                 Color.White, 
			                 MathHelper.ToRadians(Rotation - (DirectionHelper.IsDiagonal(dir.Dir) ? 45 : 0)),
			                 new Vector2(Rsc.Load<Texture2D>(sprite.Texture).Width / 4, Rsc.Load<Texture2D>(sprite.Texture).Height / 2), 
			                 SpriteEffects.None, 
			                 0);
		}
        /// <summary>
        /// Components required to act on an entity, ComponentTopDownEightDirSprite, ComponentPosition, ComponentDirection
        /// </summary>
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {
					typeof(ComponentTopDownEightDirSprite), 
					typeof(ComponentPosition),
					typeof(ComponentDirection)
				};
			}
		}
	}
}

