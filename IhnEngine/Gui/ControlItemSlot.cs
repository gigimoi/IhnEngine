using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
	public class ControlItemSlot : Control{
		public delegate void ItemSlotDelegate(ControlItemSlot control);
		private Entity _item;
		public Entity Item {
			get {
				return _item;
			}
			set {
				_item = value;
				if(value == null && ItemRemoved != null) {
					ItemRemoved.Invoke(this);
				}
				if(value != null && ItemAdded != null) {
					ItemAdded.Invoke(this);
				}
			}
		}
        public static string DefaultImg = "";
		public string Img;
		public ItemSlotDelegate ItemAdded;
		public ItemSlotDelegate ItemRemoved;
		public string ContentsType = "";
		public ControlItemSlot(Control parent, string img) : base(parent, new Vector2(36, 36)) {
			MousePressed += GrabItem;
			MouseReleased -= Control.ReturnItem;
			MouseReleased += TakeItem;
            this.Img = img;
		}
		public override void Render(Ihn ihn, SpriteBatch spriteBatch) {
			if(Item == null && DefaultImg != "") {
				var img = Rsc.Load<Texture2D>(Img);
				spriteBatch.Draw(img, new Rectangle((int)Root.X, (int)Root.Y, img.Width, img.Height), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
			}
			else {
                var img = Rsc.Load<Texture2D>(DefaultImg);
				spriteBatch.Draw(img, new Rectangle((int)Root.X, (int)Root.Y, img.Width, img.Height), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
			}
			if(Item != null) {
				if(Item.HasComp<ComponentInventorySprite>()) {
					Item.GetComp<ComponentInventorySprite>().Renderer.Invoke(ihn, Item, spriteBatch, (int)(Root.X + 2), (int)(Root.Y + 2));
				}
			}
		}
		public void GrabItem(Control control) {
			var obj = (control as ControlItemSlot);
			Gui.ItemTakenFrom = this;
			Gui.ItemMoving = obj.Item;
			obj.Item = null;
			if(ItemRemoved != null) {
				ItemRemoved.Invoke(obj);
			}
		}
		public void TakeItem(Control control) {
			if(Gui.ItemMoving != null) {
				if(ContentsType == "" || (Gui.ItemMoving.HasComp<ComponentEquipable>() && Gui.ItemMoving.GetComp<ComponentEquipable>().Slots.Contains(ContentsType))) {
					var obj = (control as ControlItemSlot);
					if(obj.Item == null) {
						Gui.ItemTakenFrom = null;
						obj.Item = Gui.ItemMoving;
					}
					else {
						Gui.ItemTakenFrom.Item = obj.Item;
						obj.Item = Gui.ItemMoving;
					}
					if(ItemAdded != null) {
						ItemAdded.Invoke(obj);
					}
					Gui.ItemMoving = null;
				}
				else {
					var obj = (control as ControlItemSlot);
					//TODO: Drop item if old slot is taken
					Gui.ItemTakenFrom.Item = Gui.ItemMoving;
					Gui.ItemMoving = null;
				}
			}
		}
	}
}
