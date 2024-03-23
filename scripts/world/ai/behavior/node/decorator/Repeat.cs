using Godot;
using System;

namespace BehaviorTree
{
    [GlobalClass]
    public partial class Repeat : Decorator
    {
        public int Limit { get; protected set; }
        protected int _counter = 0;

        public override void OnInitialize(IBehaviorTree tree)
        {
            _counter = 0;
        }

        public override Status Update(IBehaviorTree tree)
        {
            for (; ; )
            {
                _child.Tick(tree);
                if (_child.Status == Status.Running)
                {
                    break;
                }
                if (_child.Status == Status.Failure)
                {
                    return Status.Failure;
                }
                if (++_counter == Limit)
                {
                    return Status.Success;
                }
                _child.Reset();
            }
            return Status.Invalid;
        }
    }
}
