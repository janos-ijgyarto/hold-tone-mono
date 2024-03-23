using Godot;

namespace BehaviorTree
{
    [GlobalClass]
    public partial class Selector : Composite
    {
        protected int _selectedIndex;

        protected Behavior SelectedChild => _children[_selectedIndex];

        public override void OnInitialize(IBehaviorTree tree)
        {
            InitSelector();
        }

        public override Status Update(IBehaviorTree tree)
        {
            return UpdateSelector(tree);
        }

        public override void OnTerminate(IBehaviorTree tree, Status status)
        {
            // Not supposed to have any children running once we terminate
            foreach (Behavior currentChild in _children)
            {
                if (currentChild.Running == true)
                {
                    currentChild.Abort(tree);
                }
            }
        }

        protected void InitSelector()
        {
            _selectedIndex = 0;
        }

        protected Status UpdateSelector(IBehaviorTree tree)
        {
            // Keep going until a child behavior says its running.
            for (; _selectedIndex < _children.Length; ++_selectedIndex)
            {
                Status currentStatus = SelectedChild.Tick(tree);

                // If the child succeeds, or keeps running, do the same.
                if (currentStatus != Status.Failure)
                {
                    return currentStatus;
                }
            }

            // Hit the end of the array, it didn't end well...
            return Status.Failure;
        }
    }
}
