using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework;
using System.Threading;

namespace IhnLib {
    /// <summary>
    /// Manages loading and unloading resources
    /// </summary>
	public static class Rsc {
		static Dictionary<Type, Dictionary<string, object>> rscs = new Dictionary<Type, Dictionary<string, object>>();
		
        /// <summary>
        /// Loads resource of type
        /// </summary>
        /// <typeparam name="T">Type of resource to load</typeparam>
        /// <param name="path">Path to load resource from</param>
        /// <returns>Loaded resource</returns>
        public static T Load<T>(string path) {
			if(!rscs.ContainsKey(typeof(T))) {
				rscs.Add(typeof(T), new Dictionary<string, object>());
			}
			if(path != "" && path != null) {
				if(!rscs[typeof(T)].ContainsKey(path)) {
					var newPath = Path.GetFullPath(path);
					var dat = Ihn.Instance.Content.Load<T>(newPath);

					if(dat.GetType() == typeof(Texture2D)) {
						(dat as Texture2D).Name = path;
					}
					rscs[typeof(T)].Add(path, dat);
				}
				return (T)rscs[typeof(T)][path];
			}
			if(typeof(T) == typeof(Texture2D)) {
				if(!rscs[typeof(T)].ContainsKey("NULL_DATA")) {
					rscs[typeof(T)].Add("NULL_DATA", Art.GetNone());
				}
				return (T)rscs[typeof(T)]["NULL_DATA"];
			}
			return default(T);
		}
        /// <summary>
        /// Places one texture ontop of another and returns it
        /// </summary>
        /// <param name="spriteOne">Sprite to place spritetwo atop</param>
        /// <param name="spriteTwo">Sprite to place atop spriteone</param>
        /// <returns>Combined sprite</returns>
		public static Texture2D CombineTextures(Texture2D spriteOne, Texture2D spriteTwo) {
			Color[] CombinedColorData = new Color[spriteOne.Width * spriteOne.Height];
			Color[] TempColorData = new Color[spriteTwo.Width * spriteTwo.Height];
			spriteOne.GetData(CombinedColorData);
			spriteTwo.GetData(TempColorData);
            for (int i = 0; i < CombinedColorData.Length; i++) {
                if (TempColorData[i].A != 0) {
                    CombinedColorData[i] = TempColorData[i];
                }
            }
			Texture2D returnTexture = new Texture2D(Ihn.Instance.GraphicsDevice, Math.Max(spriteOne.Width, spriteTwo.Width), Math.Max(spriteOne.Height, spriteTwo.Height));
			returnTexture.SetData(CombinedColorData);

			return returnTexture;
		}
	}
}