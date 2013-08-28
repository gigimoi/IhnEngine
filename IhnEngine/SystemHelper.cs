//
// SystemHelper.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using System.Collections.Generic;

namespace IhnLib {
	public static class SystemHelper {
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

