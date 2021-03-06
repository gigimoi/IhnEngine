using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
    /// <summary>
    /// GUI Control, subtyped to create complex GUI elements
    /// </summary>
	public class Control {
        /// <summary>
        /// Delegate to manage mouse events
        /// </summary>
        /// <param name="mouseOver"></param>
		public delegate void ControlDelegate(Control mouseOver);
        /// <summary>
        /// List of children
        /// </summary>
		public List<Control> Children = new List<Control>();
        /// <summary>
        /// Control this control is a child of
        /// </summary>
		public Control Parent;
        /// <summary>
        /// Render/Update this control if true
        /// </summary>
		public bool Enabled = true;
        /// <summary>
        /// Offset in pixels from the parent control
        /// </summary>
		public Vector2 Offset = new Vector2(0, 0);
        /// <summary>
        /// Size of this control, used for mouse events
        /// </summary>
		public Vector2 Size = new Vector2(0, 0);
        /// <summary>
        /// Events to run whenever mouse is over this control
        /// </summary>
		public ControlDelegate MouseOver;
        /// <summary>
        /// Events to run whenever mouse left is pressed on this control
        /// </summary>
		public ControlDelegate MousePressed;
        /// <summary>
        /// Events to run whenever mouse left is released on this control
        /// </summary>
		public ControlDelegate MouseReleased;
        /// <summary>
        /// Events to run whenever mouse left is down on this control
        /// </summary>
		public ControlDelegate MouseDown;
        /// <summary>
        /// Events to run when the mouse first enters this control
        /// </summary>
		public ControlDelegate MouseEnter;
        /// <summary>
        /// Events to run when the mouse first leaves this control
        /// </summary>
		public ControlDelegate MouseExit;
        /// <summary>
        /// Object of data that can be manipulated
        /// </summary>
		public object Tag = new Object();

		private bool _mouseOver = false;

        /// <summary>
        /// Instantiates a control with a parent and size
        /// </summary>
        /// <param name="parent">Control this control is a child of</param>
        /// <param name="size">Width+Height of this control</param>
		public Control(Control parent, Vector2 size) {
			Parent = parent;
			if(parent != null) {
				parent.Children.Add(this);
			}
			Size = size;
			MouseReleased += ReturnItem;
		}

        /// <summary>
        /// Searched for a child with a tag equaling the input
        /// </summary>
        /// <param name="tag">Object to search for</param>
        /// <returns>Child that has the input tag</returns>
		public Control GetChildWithTag(object tag) {
			for(int i = 0; i < Children.Count; i++) {
				if(Children[i].Tag == tag) {
					return Children[i];
				}
			}
			return null;
		}

        /// <summary>
        /// Returns an item to where it was taken from
        /// </summary>
        /// <param name="mouseOver">Control the mouse is over</param>
		public static void ReturnItem(Control mouseOver) {
			if(Gui.ItemMoving != null) {
				Gui.ItemTakenFrom.Item = Gui.ItemMoving;
				Gui.ItemMoving = null;
				Gui.ItemTakenFrom = null;
			}
		}
        /// <summary>
        /// Gets the literael position of the top left corner of this gui element
        /// </summary>
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
        /// <summary>
        /// Updates status of gui control
        /// </summary>
        /// <param name="ihn">Ihn that triggered the update</param>
		public virtual void Update(Ihn ihn) {
			for(int i = 0; i < Children.Count; i++) {
				if(Children[i].Enabled) {
					Children[i].Update(ihn);
				}
			}
			if(Enabled) {
				var shouldInvokeMouseEvents = true;
				for(int i = 0; i < Children.Count; i++) {
					if(Children[i].Enabled) {
						if(new Rectangle(MouseHelper.X, MouseHelper.Y, 1, 1).Intersects(new FloatRect(Children[i].Root.X, Children[i].Root.Y, Children[i].Size.X, Children[i].Size.Y).ToRect())) {
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
        /// <summary>
        /// Renders this control
        /// </summary>
        /// <param name="ihn">Ihn calling the render</param>
        /// <param name="spriteBatch">Spritebatch to render with</param>
        public virtual void Render(Ihn ihn, SpriteBatch spriteBatch) {
        }
        /// <summary>
        /// Returns a list of renders to perform
        /// </summary>
        /// <returns>List of renders to perform</returns>
        public List<List<Control>> GetRenders() {
            return GetRenders(new List<List<Control>>(), 0);
        }
        /// <summary>
        /// Subprocess of GetRenders, do not call
        /// </summary>
        /// <param name="list">List of renders</param>
        /// <param name="depth">Depth in gui tree</param>
        /// <returns>Updated list</returns>
        public List<List<Control>> GetRenders(List<List<Control>> list, int depth) {
            if (list.Count <= depth) {
                list.Add(new List<Control>());
            }
            list[depth].Add(this);
            for (int i = 0; i < Children.Count; i++) {
                if (Children[i].Enabled) {
                    list = Children[i].GetRenders(list, depth + 1);
                }
            }
            return list;
        }
	}
}
