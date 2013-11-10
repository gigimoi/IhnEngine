using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace IhnLib {
    /// <summary>
    /// Allows an entity to perform an action when clicked
    /// </summary>
	[Serializable]
	public class SystemOnClickedDo : ISystem {
        /// <summary>
        /// Checks for a click action and runs the action if the entity is clicked
        /// </summary>
        /// <param name="ihn">Ihn entity is contained in</param>
        /// <param name="entity">Entity to update</param>
		public void Update(Ihn ihn, Entity entity) {
			var pos = entity.GetComp<ComponentPosition>();
			var x = pos.X;
			var y = pos.Y;
			var w = 0;
			var h = 0;
			if(entity.HasComp<ComponentAABB>()) {
				var aabb = entity.GetComp<ComponentAABB>();
				x = aabb.X;
				y = aabb.Y;
				w = aabb.Width;
				h = aabb.Height;
			}
			else if(entity.HasComp<ComponentTopDownEightDirSprite>()) {
				var spr = entity.GetComp<ComponentTopDownEightDirSprite>();
				x -= Rsc.Load<Texture2D>(spr.Texture).Width / 8;
				y -= Rsc.Load<Texture2D>(spr.Texture).Height / 4;
				w = Rsc.Load<Texture2D>(spr.Texture).Width / 4;
				h = Rsc.Load<Texture2D>(spr.Texture).Height / 2;
			}
			else if(entity.HasComp<ComponentSize>()) {
				w = entity.GetComp<ComponentSize>().Width;
				h = entity.GetComp<ComponentSize>().Height;
			}
			else if(entity.HasComp<ComponentSprite>()) {
				var spr = entity.GetComp<ComponentSprite>();
				x -= spr.Origin.X;
				y -= spr.Origin.Y;
				w = Rsc.Load<Texture2D>(spr.Texture).Width;
				h = Rsc.Load<Texture2D>(spr.Texture).Height;
			}
			var onclick = entity.GetComp<ComponentOnClickedDo>();
			if(MouseHelper.MouseLeftPressed()) {
				if(MouseHelper.X > x && MouseHelper.X < x + w) {
					if(MouseHelper.Y > y && MouseHelper.Y < y + h) {
						onclick.Todo.Invoke(this, new EventArgs());
					}
				}
			}
		}
        /// <summary>
        /// This system does not render
        /// </summary>
        /// <param name="ihn">Ihn entity is contained in</param>
        /// <param name="spriteBatch">Spritebatch to draw with</param>
        /// <param name="entity">Entity to draw</param>
		public void Render(Ihn ihn, SpriteBatch spriteBatch, Entity entity) {
		}
        /// <summary>
        /// This system requires ComponentOnClickedDo and ComponentPosition
        /// </summary>
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {
					typeof(ComponentOnClickedDo),
					typeof(ComponentPosition)
				};
			}
		}
	}
}

