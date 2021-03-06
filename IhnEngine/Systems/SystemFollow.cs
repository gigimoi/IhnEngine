using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace IhnLib {
    /// <summary>
    /// Allows entities to snap directly to another entities position
    /// </summary>
	[Serializable]
	public class SystemFollow : ISystem{
        /// <summary>
        /// Snaps the entity to its follow
        /// </summary>
        /// <param name="ihn">Ihn entity is contained in</param>
        /// <param name="entity">Entity to update</param>
		public void Update(Ihn ihn, Entity entity) {
			var pos = entity.GetComp<ComponentPosition>();
			var follow = entity.GetComp<ComponentFollow>();

			if(follow.ToFollow.HasComp<ComponentPosition>()) {
				pos.X = follow.ToFollow.GetComp<ComponentPosition>().X + follow.Offset.X;
				pos.Y = follow.ToFollow.GetComp<ComponentPosition>().Y + follow.Offset.Y;
			}
			if (follow.FollowRotation) {
				var rot = 0f;
				if (follow.ToFollow.HasComp<ComponentMultiSprite>()) {
					rot = follow.ToFollow.GetComp<ComponentMultiSprite>().Rotation;
				}
				else if (follow.ToFollow.HasComp<ComponentSprite>()) {
					rot = follow.ToFollow.GetComp<ComponentSprite>().Rotation;
				}
				if (entity.HasComp<ComponentMultiSprite>()) {
					entity.GetComp<ComponentMultiSprite>().Rotation = rot;
				}
				if (entity.HasComp<ComponentSprite>()) {
					entity.GetComp<ComponentSprite>().Rotation = rot;
				}
			}
			if (follow.FollowDestroy) {
				if (!ihn.ContainsEntity(follow.ToFollow)) {
					Ihn.Instance.RemoveEntity(entity);
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
        /// Components this system requires, ComponentPosition and ComponentFollow
        /// </summary>
		public List<Type> RequiredComponents {
			get {
				return new List<Type>() {
					typeof(ComponentPosition),
					typeof(ComponentFollow)
				};
			}
		}
	}
}

