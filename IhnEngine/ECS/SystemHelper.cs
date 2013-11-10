//
using System;
using System.Collections.Generic;

namespace IhnLib {
    /// <summary>
    /// Aids with systems management
    /// </summary>
	public static class SystemHelper {
        /// <summary>
        /// Checks if a system is able to run on an entity
        /// </summary>
        /// <param name="system">System to test with</param>
        /// <param name="entity">Entity to test on</param>
        /// <returns>Whether the system can run</returns>
		public static bool CanSystemRunOnEntity(ISystem system, Entity entity) {
			var reqComps = system.RequiredComponents;
			for(int i = 0; i < entity.Components.Count; i++) {
				for(int j = 0; j < reqComps.Count; j++) {
					if(entity.HasComp(reqComps[j])) {
						reqComps.Remove(reqComps[j]);
					}
				}
			}
			return reqComps.Count == 0;
		}
	}
}

