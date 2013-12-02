using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace IhnLib {
    /// <summary>
    /// Handles all gui elements
    /// </summary>
	public class Gui {
        /// <summary>
        /// The control which all others should be added to
        /// </summary>
		public static Control RootControl = new Control(null, new Vector2(10000, 10000));
        /// <summary>
        /// In item systems, this is the entity being dragged between windows
        /// </summary>
		public static Entity ItemMoving;
        /// <summary>
        /// Control where ItemMoving was taken from
        /// </summary>
		public static ControlItemSlot ItemTakenFrom;
		private static bool listening = false;
        /// <summary>
        /// Image to render in place of the mouse cursor
        /// </summary>
        public static string CursorImg;
        /// <summary>
        /// Currently active control
        /// </summary>
		public static Control Focus;
        /// <summary>
        /// Refreshes listeners, must be called once
        /// </summary>
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
			if (Gui.Focus != null && Gui.Focus.Enabled == false) {
				Gui.Focus = null;
			}
			if(RootControl.Enabled) {
				RootControl.Update(Ihn.Instance);
			}
#if DEBUG
			if(KeyHelper.KeyDown(Keys.F4) && (KeyHelper.KeyDown(Keys.LeftAlt) || KeyHelper.KeyDown(Keys.RightAlt))) {
				Ihn.Instance.Exit();
			}
#endif
		}
		private void Render(object sender, EventArgs e) {
            if (RootControl != null && RootControl.Enabled) {
                var renders = RootControl.GetRenders();
                for (int i = 0; i < renders.Count; i++) {
                    for (int j = 0; j < renders[i].Count; j++) {
                        renders[i][j].Render(Ihn.Instance, Ihn.Instance.SBatch);
                    }
                }
            }
			if(ItemMoving != null && ItemMoving.HasComp<ComponentInventorySprite>()) {
				ItemMoving.GetComp<ComponentInventorySprite>().Renderer.Invoke(Ihn.Instance, ItemMoving, Ihn.Instance.SBatch, MouseHelper.X - 16, MouseHelper.Y - 16);
			}
			else if(!Ihn.Instance.IsMouseVisible){
                Ihn.Instance.SBatch.Draw(Rsc.Load<Texture2D>(CursorImg), new Rectangle((int)(MouseHelper.X - 8), (int)(MouseHelper.Y - 8), 16, 16), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, (int)0);
			}
		}
	}
}

