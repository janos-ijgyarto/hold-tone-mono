using Godot;
using System;

namespace BehaviorTree
{
	[GlobalClass]
	public partial class IsInRange : Condition
	{
        [Export]
        private float _range = 5.0f;

        [Export]
        private string _positionKey = "position";

        [Export]
        private string _targetKey = "target";

		protected override bool EvaluateCondition(IBehaviorTree tree)
		{
            Vector2 position = tree.Blackboard.Get<Vector2>(_positionKey);
            Vector2 target = tree.Blackboard.Get<Vector2>(_targetKey);

            if (position.DistanceSquaredTo(target) <= (_range * _range))
            {
                return true;
            }

            return false;
        }
    }
}