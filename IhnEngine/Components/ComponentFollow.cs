using System;
using Microsoft.Xna.Framework;

namespace IhnLib {
	/// <summary>
	/// Stores data for SystemFollow
	/// </summary>
	[Serializable]
	public class ComponentFollow : Component{
		/// <summary>
		/// Entity this should follow
		/// </summary>
		public Entity ToFollow;
		/// <summary>
		/// Offset to the other entities position
		/// </summary>
		public Position Offset;

		/// <summary>
		/// True if the entity housing this component should match the rotation of the entity it is following
		/// </summary>
		public bool FollowRotation;

		/// <summary>
		/// True if the entity housing this component should destroy with ToFollow
		/// </summary>
		public bool FollowDestroy;

		/// <summary>
		/// Instantiates ComponentFollow
		/// </summary>
		/// <param name="toFollow">Entity this should follow</param>
		/// <param name="offset">Offset to the other entities position</param>
		/// <param name="followRotation">Should this entity match rotation</param>
		/// <param name="followDestroy">Should this entity destroy with the followed entity</param>
		public ComponentFollow(Entity toFollow, Position offset, bool followRotation = false, bool followDestroy = false) {
			if(!toFollow.HasComp<ComponentPosition>()) {
				Console.WriteLine("WARNING: Trying to follow component without position");
			}
			FollowRotation = followRotation;
			ToFollow = toFollow;
			Offset = offset;
			FollowDestroy = followDestroy;
		}
		/// <summary>
		/// Instantiates ComponentFollow
		/// </summary>
		/// <param name="toFollow">Entity this should follow</param>
		public ComponentFollow(Entity toFollow) : this(toFollow, new Position(0, 0)) { }
	}
}

