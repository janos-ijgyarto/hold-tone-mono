using Godot;
using System;

namespace BehaviorTree
{
    public partial class PickRandomDestination : Behavior
    {
        [Export]
        private float _radius = 200.0f;

        [Export]
        private string _positionKey = "position";

        [Export]
        private string _destinationKey = "destination"; // FIXME: have proper system for linking BB inputs to outputs?

        public override Status Update(IBehaviorTree tree)
        {
            // Pick a random destination
            // FIXME: provide parameters for more complex destination choice, for now we'll just have it orbit the center

            // Start on the opposite side of current position
            Vector2 newDestination = tree.Blackboard.Get<Vector2>(_positionKey);

            newDestination = -(newDestination.Normalized());

            // Rotate by random amount, choose random scale w.r.t radius
            newDestination = newDestination.Rotated((float)GD.RandRange(-Math.PI * 0.4, Math.PI * 0.4)) * (float)GD.RandRange(_radius * 0.25, _radius);

            tree.Blackboard.Set(_destinationKey, newDestination);

            return Status.Success;
        }
    }
}