using Godot;
using System;

namespace BehaviorTree
{
    [GlobalClass]
    public partial class MockBehavior : Behavior
    {
        private int _initializeCalled = 0;
        private int _terminateCalled = 0;
        private int _updateCalled = 0;
        private Status _returnStatus = Status.Running;
        private Status _terminateStatus = Status.Invalid;

        public override void OnInitialize(IBehaviorTree tree)
        {
            ++_initializeCalled;
        }

        public override void OnTerminate(IBehaviorTree tree, Status status)
        {
            ++_terminateCalled;
            _terminateStatus = status;
        }

        public override Status Update(IBehaviorTree tree)
        {
            ++_updateCalled;
            return _returnStatus;
        }
    }
}

