using Godot;

namespace BehaviorTree
{
    public abstract partial class Decorator : Behavior
    {
        protected Behavior _child;

        public override void _Ready()
        {
            if (GetChildren().Count > 1)
            {
                GD.PushError("BEHAVIOR TREE ERROR: invalid tree configuration for Decorator!");
            }

            Node child = GetChild(0);

            if (child is Behavior)
            {
                _child = child as Behavior;
            }
            else
            {
                GD.PushError("BEHAVIOR TREE ERROR: Decorator child must be Behavior!");
            }
        }
    }
}