using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
	public class Control {
		public delegate void ControlDelegate(Control mouseOver);
		public List<Control> Controls = new List<Control>();
		public Control Parent;
		public bool Enabled = true;
		public Vector2 Offset = new Vector2(0, 0);
		public Vector2 Size = new Vector2(0, 0);
		public ControlDelegate MouseOver;
		public ControlDelegate MousePressed;
		public ControlDelegate MouseReleased;
		public ControlDelegate MouseDown;
		public ControlDelegate MouseEnter;
		public ControlDelegate MouseExit;
		public object Tag = new Object();

		private bool _mouseOver = false;

		public Control(Control parent, Vector2 size) {
			Parent = parent;
			if(parent != null) {
				parent.Controls.Add(this);
			}
			Size = size;
			MouseReleased += ReturnItem;
		}

		public Control GetChildWithTag(object tag) {
			for(int i = 0; i < Controls.Count; i++) {
				if(Controls[i].Tag == tag) {
					return Controls[i];
				}
			}
			return null;
		}

		public static void ReturnItem(Control mouseOver) {
			if(Gui.ItemMoving != null) {
				Gui.ItemTakenFrom.Item = Gui.ItemMoving;
				Gui.ItemMoving = null;
				Gui.ItemTakenFrom = null;
			}
		}
		public Vector2 Root {
			get {
				if(Parent == null) {
                    return new Vector2(Offset.X, Offset.Y);
				}
				else {
					var ovector = Parent.Root;
					ovector.X += Offset.X;
					ovector.Y += Offset.Y;
					return ovector;
				}
			}
		}
		public virtual void Update(Ihn ihn) {
			for(int i = 0; i < Controls.Count; i++) {
				if(Controls[i].Enabled) {
					Controls[i].Update(ihn);
				}
			}
			if(Enabled) {
				var shouldInvokeMouseEvents = true;
				for(int i = 0; i < Controls.Count; i++) {
					if(Controls[i].Enabled) {
						if(new Rectangle(MouseHelper.X, MouseHelper.Y, 1, 1).Intersects(new FloatRect(Controls[i].Root.X, Controls[i].Root.Y, Controls[i].Size.X, Controls[i].Size.Y).ToRect())) {
							shouldInvokeMouseEvents = false;
							if(_mouseOver) {
								_mouseOver = false;
								if(MouseExit != null) {
									MouseExit.Invoke(this);
								}
							}
							break;
						}
					}
				}
				if(shouldInvokeMouseEvents) {
					InvokeMouseEvents();
				}
			}
		}
		private void InvokeMouseEvents() {
			if(new Rectangle(MouseHelper.X, MouseHelper.Y, 1, 1).Intersects(new FloatRect(Root.X, Root.Y, Size.X, Size.Y).ToRect())) {
				if(MouseOver != null) {
					MouseOver.Invoke(this);
				}
				if(!_mouseOver) {
					if(MouseEnter != null) {
						MouseEnter.Invoke(this);
					}
				}
				_mouseOver = true;
				if(MouseHelper.MouseLeftDown()) {
					if(MouseDown != null) {
						MouseDown.Invoke(this);
					}
				}
				if(MouseHelper.MouseLeftPressed()) {
					if(MousePressed != null) {
						MousePressed.Invoke(this);
					}
				}
				if(MouseHelper.MouseLeftReleased()) {
					if(MouseReleased != null) {
						MouseReleased.Invoke(this);
					}
				}
			}
			else if(_mouseOver) {
				_mouseOver = false;
				if(MouseExit != null) {
					MouseExit.Invoke(this);
				}
			}
		}
		public virtual void Render(Ihn ihn, SpriteBatch spriteBatch) {
			for(int i = 0; i < Controls.Count; i++) {
				if(Controls[i].Enabled) {
					Controls[i].Render(ihn, spriteBatch);
				}
			}
		}
	}
}
