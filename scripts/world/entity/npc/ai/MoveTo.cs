using BehaviorTree;
using Godot;
using System;

[GlobalClass]
public partial class MoveTo : Behavior
{
    [Export]
    private string _positionKey = "position";

    [Export]
    private string _destinationKey = "move_to";

    [Export]
    private string _controlKey = "control";

    public override Status Update(IBehaviorTree tree)
    {
        Blackboard blackboard = tree.Blackboard;

        Vector2 position = blackboard.Get<Vector2>(_positionKey);
        Vector2 destination = blackboard.Get<Vector2>(_destinationKey);

        if (position.DistanceSquaredTo(destination) < 0.5f)
        {
            // FIXME: should we control everything through BB, or allow low-level actions to modify game state directly?
            blackboard.Set(_controlKey, Vector2.Zero);
            return Status.Success;
        }

        blackboard.Set(_controlKey, position.DirectionTo(destination));
        return Status.Running;
    }

    public override void OnTerminate(IBehaviorTree tree, Status status)
    {
        base.OnTerminate(tree, status);
        tree.Blackboard.Set(_controlKey, Vector2.Zero);
    }
}