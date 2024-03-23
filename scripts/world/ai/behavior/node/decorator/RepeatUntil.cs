using Godot;
using System;

namespace BehaviorTree
{
    [GlobalClass]
    public partial class RepeatUntil : Decorator
    {
        [Export]
        protected Status _endStatus = Status.Failure;

        public override Status Update(IBehaviorTree tree)
        {
            _child.Tick(tree);

            if (_child.Status == _endStatus)
            {
                // Reached desired end status
                return _endStatus;
            }
            else
            {
                // Child still running (or needs to run again)
                if (_child.Status != Status.Running)
                {
                    _child.Reset();
                }

                return Status.Running;
            }
        }
    }
}
