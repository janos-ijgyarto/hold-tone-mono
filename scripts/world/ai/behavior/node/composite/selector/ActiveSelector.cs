using Godot;

namespace BehaviorTree
{
    [GlobalClass]
    public partial class ActiveSelector : Selector
    {
        public override void OnInitialize(IBehaviorTree tree)
        {
            _selectedIndex = _children.Length;
        }

        public override Status Update(IBehaviorTree tree)
        {
            int lastIndex = _selectedIndex;

            InitSelector();
            Status result = UpdateSelector(tree);

            if ((lastIndex < _children.Length) && (_selectedIndex != lastIndex))
            {
                // Swapping to higher priority node, abort previous node
                _children[lastIndex].Abort(tree);
            }
            return result;
        }
    }
}