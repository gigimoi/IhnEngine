using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace IhnLib {
	public class Gui {
		public static Control RootControl = new Control(null, new Vector2(0, 0));
		public static Entity ItemMoving;
		public static ControlItemSlot ItemTakenFrom;
		private static bool listening = false;
        public static string CursorImg;
		public Gui() {
			if(listening) {
				EventManager.UnListen("Post Ihn Update", Update);
				EventManager.UnListen("Post Ihn Render", Render);
			}
			listening = true;
			EventManager.Listen("Post Ihn Update", Update);
			EventManager.Listen("Post Ihn Render", Render);
		}

		private void Update(object sender, EventArgs e) {
			if(RootControl.Enabled) {
				RootControl.Update(Ihn.Instance);
			}
#if DEBUG
			if(KeyHelper.KeyDown(Keys.F4) && (KeyHelper.KeyDown(Keys.LeftAlt) || KeyHelper.KeyDown(Keys.RightAlt))) {
				Ihn.Instance.Exit();
			}
#endif
		}
		public void Render(object sender, EventArgs e) {
			RootControl.Render(Ihn.Instance, Ihn.Instance.SBatch);
			if(ItemMoving != null && ItemMoving.HasComp<ComponentInventorySprite>()) {
				ItemMoving.GetComp<ComponentInventorySprite>().Renderer.Invoke(Ihn.Instance, ItemMoving, Ihn.Instance.SBatch, MouseHelper.X - 16, MouseHelper.Y - 16);
			}
			else if(!Ihn.Instance.IsMouseVisible){
                Ihn.Instance.SBatch.Draw(Rsc.Load<Texture2D>(CursorImg), new Rectangle((int)(MouseHelper.X - 8), (int)(MouseHelper.Y - 8), 16, 16), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, (int)-10000);
			}
		}
	}
}

