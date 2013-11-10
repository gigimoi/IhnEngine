using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
    /// <summary>
    /// Control which holds an entity
    /// </summary>
	public class ControlItemSlot : Control{
        /// <summary>
        /// Delegate with ControlItemSlot as input
        /// </summary>
        /// <param name="control"></param>
		public delegate void ItemSlotDelegate(ControlItemSlot control);
		private Entity _item;
        /// <summary>
        /// The current item in the contorl. Automagically invokes ItemAdded and ItemRemoved
        /// </summary>
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
        /// <summary>
        /// Default image to render for the itemslot, used if no by-instance image is selected
        /// </summary>
        public static string DefaultImg = "";
        /// <summary>
        /// Image to render as the backdrop of the item slot
        /// </summary>
		public string Img;
        /// <summary>
        /// Function to run when an item is added to the slot
        /// </summary>
		public ItemSlotDelegate ItemAdded;
        /// <summary>
        /// Function to run when an item is removed from the slot
        /// </summary>
		public ItemSlotDelegate ItemRemoved;
        /// <summary>
        /// Type of items allowed into the slot
        /// </summary>
		public string ContentsType = "";
        /// <summary>
        /// Construct a new itemslot
        /// </summary>
        /// <param name="parent">Control to be a child of</param>
        /// <param name="img">backdrop image</param>
		public ControlItemSlot(Control parent, string img) : base(parent, new Vector2(36, 36)) {
			MousePressed += GrabItem;
			MouseReleased -= Control.ReturnItem;
			MouseReleased += TakeItem;
            this.Img = img;
		}
        /// <summary>
        /// Renders the control and the item in it
        /// </summary>
        /// <param name="ihn">Ihn the control is rendered in</param>
        /// <param name="spriteBatch">Spritebatch to render with</param>
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
        /// <summary>
        /// Automatically called to take an item from the slot with the mouse
        /// </summary>
        /// <param name="control"></param>
		public void GrabItem(Control control) {
			var obj = (control as ControlItemSlot);
			Gui.ItemTakenFrom = this;
			Gui.ItemMoving = obj.Item;
			obj.Item = null;
			if(ItemRemoved != null) {
				ItemRemoved.Invoke(obj);
			}
		}
        /// <summary>
        /// Accepts an item into the slot
        /// </summary>
        /// <param name="control">Control used to take item</param>
		public void TakeItem(Control control) {
			if(Gui.ItemMoving != null) {
				if(ContentsType == "" || (Gui.ItemMoving.HasComp<ComponentSlotted>() && Gui.ItemMoving.GetComp<ComponentSlotted>().Slots.Contains(ContentsType))) {
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
