using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
	public class BehaviorTreeNode {
		public BehaviorTreeNode(BehaviorTreeNode parent) {
			Parent = parent;
			if(parent != null) {
				parent.Children.Add(this);
			}
		}
		public virtual bool Visit() {
			return false;
		}
		public BehaviorTreeNode Parent;
		public List<BehaviorTreeNode> Children = new List<BehaviorTreeNode>();
	}
	public class BTNodePriority : BehaviorTreeNode {
		public BTNodePriority(BehaviorTreeNode parent) : base(parent) { }
		public override bool Visit() {
			for(int i = 0; i < Children.Count; i++) {
				if(!Children[i].Visit()) {
					return false;
				}
			}
			return true;
		}
	}
	public class BTNodeSequence : BehaviorTreeNode {
		public BTNodeSequence(BehaviorTreeNode parent) : base(parent) { }
		public override bool Visit() {
			for(int i = 0; i < Children.Count; i++) {
				Children[i].Visit();
			}
			return true;
		}
	}
	public class BTNodeAction : BehaviorTreeNode {
		public Func<object, bool> DGate;
		public object Obj;

		public BTNodeAction(BehaviorTreeNode parent, Func<object, bool> dgate, object obj)
			: base(parent) {
			DGate = dgate;
			Obj = obj;
		}
		public override bool Visit() {
			return DGate.Invoke(Obj);
		}
	}
}
