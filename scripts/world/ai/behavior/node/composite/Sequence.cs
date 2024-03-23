using Godot;
using System;
using System.Collections.Generic;

namespace BehaviorTree
{
    [GlobalClass]
    public partial class Sequence : Composite
    {
        private int _childIndex;

        protected Behavior CurrentChild => _children[_childIndex];

        public override void OnInitialize(IBehaviorTree tree)
        {
            _childIndex = 0;
        }

        public override Status Update(IBehaviorTree tree)
        {
            // Keep going until a child behavior says it's running.
            for (; _childIndex < _children.Length; ++_childIndex)
            {
                Status currentStatus = CurrentChild.Tick(tree);

                // If the child fails, or keeps running, do the same.
                if (currentStatus != Status.Success)
                {
                    return currentStatus;
                }
            }

            // Hit the end of the array, job done!
            return Status.Success;
        }

        public override void OnTerminate(IBehaviorTree tree, Status status)
        {
            foreach (Behavior currentChild in _children)
            {
                if (currentChild.Running == true)
                {
                    currentChild.Abort(tree);
                }
            }
        }
    }
}
