using Godot;
using System;

namespace BehaviorTree
{
	public partial class Parallel : Composite
	{
        public enum Policy
        {
            RequireOne,
            RequireAll,
        }

        [Export]
        protected Policy _successPolicy = Policy.RequireAll;

        [Export]
        protected Policy _failurePolicy = Policy.RequireOne;

        public override void OnInitialize(IBehaviorTree tree)
        {
            foreach (Behavior currentChild in _children)
            {
                currentChild.Reset();
            }
        }

        public override Status Update(IBehaviorTree tree)
        {
            int successCount = 0;
            int failureCount = 0;

            foreach(Behavior currentChild in _children)
            {
                if (currentChild.Terminated == false)
                {
                    currentChild.Tick(tree);
                }

                if (currentChild.Status == Status.Success)
                {
                    ++successCount;
                    if (_successPolicy == Policy.RequireOne)
                    {
                        return Status.Success;
                    }
                }

                if (currentChild.Status == Status.Failure)
                {
                    ++failureCount;
                    if (_failurePolicy == Policy.RequireOne)
                    {
                        return Status.Failure;
                    }
                }
            }

            if ((_failurePolicy == Policy.RequireAll) && (failureCount == _children.Length))
            {
                return Status.Failure;
            }

            if ((_successPolicy == Policy.RequireAll) && (successCount == _children.Length))
            {
                return Status.Success;
            }

            return Status.Running;
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
