using Godot;
using System;

namespace BehaviorTree
{
    public enum Status
    {
        Invalid,
        Success,
        Failure,
        Running,
        Aborted
    }

    public abstract partial class Behavior : Node
    {
        public Status Status { get; protected set; } = Status.Invalid;

        public bool Terminated => Status == Status.Success || Status == Status.Failure;
        public bool Running => Status == Status.Running;

        public abstract Status Update(IBehaviorTree tree);

        public virtual void OnInitialize(IBehaviorTree tree) { }
        public virtual void OnTerminate(IBehaviorTree tree, Status status) { }

        public Status Tick(IBehaviorTree tree)
        {
            if (Status != Status.Running)
            {
                OnInitialize(tree);
            }

            Status = Update(tree);

            if (Status != Status.Running)
            {
                OnTerminate(tree, Status);
            }

            return Status;
        }

        public void Reset()
        {
            Status = Status.Invalid;
        }

        public void Abort(IBehaviorTree tree)
        {
            OnTerminate(tree, Status.Aborted);
            Status = Status.Aborted;
        }
    }

}