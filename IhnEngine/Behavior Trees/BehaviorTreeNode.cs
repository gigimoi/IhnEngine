using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
    /// <summary>
    /// Generic node in a behavior tree
    /// </summary>
	public class BehaviorTreeNode {
        /// <summary>
        /// Creates a new behavior tree node that comes from parent. Added to the end of the parents children
        /// </summary>
        /// <param name="parent">Node that this one is a child of</param>
		public BehaviorTreeNode(BehaviorTreeNode parent) {
			Parent = parent;
			if(parent != null) {
				parent.Children.Add(this);
			}
		}
        /// <summary>
        /// Called when a node should perform its duty
        /// </summary>
        /// <returns>False</returns>
		public virtual bool Visit() {
			return false;
		}
        /// <summary>
        /// Node that this node is a child of
        /// </summary>
		public BehaviorTreeNode Parent;
        /// <summary>
        /// List of nodes that this node gave birth to
        /// </summary>
		public List<BehaviorTreeNode> Children = new List<BehaviorTreeNode>();
	}
    /// <summary>
    /// Node that runs all of its children in order until one fails
    /// </summary>
	public class BTNodePriority : BehaviorTreeNode {
        /// <summary>
        /// Creates a new behavior tree node that comes from parent. Added to the end of the parents children
        /// </summary>
        /// <param name="parent">Node that this one is a child of</param>
		public BTNodePriority(BehaviorTreeNode parent) : base(parent) { }
        /// <summary>
        /// Runs all children in order until one fails
        /// </summary>
        /// <returns>False if any child returns false</returns>
		public override bool Visit() {
			for(int i = 0; i < Children.Count; i++) {
				if(!Children[i].Visit()) {
					return false;
				}
			}
			return true;
		}
	}
    /// <summary>
    /// Runs all children regardless of outcome
    /// </summary>
    public class BTNodeSequence : BehaviorTreeNode {
        /// <summary>
        /// Creates a new behavior tree node that comes from parent. Added to the end of the parents children
        /// </summary>
        /// <param name="parent">Node that this one is a child of</param>
        public BTNodeSequence(BehaviorTreeNode parent) : base(parent) { }
        /// <summary>
        /// Runs all children in order even if one fails
        /// </summary>
        /// <returns>True</returns>
        public override bool Visit() {
            for (int i = 0; i < Children.Count; i++) {
                Children[i].Visit();
            }
            return true;
        }
    }
    /// <summary>
    /// Performs an action on an object when visited
    /// </summary>
	public class BTNodeAction : BehaviorTreeNode {
        /// <summary>
        /// Function to run
        /// </summary>
		public Func<object, bool> DGate;
        /// <summary>
        /// Object to act on
        /// </summary>
		public object Obj;

        /// <summary>
        /// Creates a new behavior tree node that comes from parent. Added to the end of the parents children
        /// </summary>
        /// <param name="parent">Node that this one is a child of</param>
        /// <param name="dgate">Function to run</param>
        /// <param name="obj">Ojbect to run on</param>
		public BTNodeAction(BehaviorTreeNode parent, Func<object, bool> dgate, object obj)
			: base(parent) {
			DGate = dgate;
			Obj = obj;
		}
        /// <summary>
        /// Invokes function on object
        /// </summary>
        /// <returns>Function output</returns>
		public override bool Visit() {
			return DGate.Invoke(Obj);
		}
	}
}
