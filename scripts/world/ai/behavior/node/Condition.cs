using Godot;
using System;

namespace BehaviorTree
{
    public abstract partial class Condition : Behavior
    {
        public override Status Update(IBehaviorTree tree)
        {
            return EvaluateCondition(tree) ? Status.Success : Status.Failure;
        }

        protected abstract bool EvaluateCondition(IBehaviorTree tree);
    }
}
