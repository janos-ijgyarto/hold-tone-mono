using Godot;
using System;

namespace BehaviorTree
{
    public interface IBehaviorTree
    {
        public Blackboard Blackboard { get; }
    }

    [GlobalClass]
    public partial class BehaviorTree : Node, IBehaviorTree
    {
        [Export]
        private PackedScene _rootPrefab;

        private Behavior _rootBehavior;
        private Blackboard _blackboard;

        public Blackboard Blackboard => _blackboard;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _rootBehavior = _rootPrefab.Instantiate<Behavior>();
            AddChild(_rootBehavior);
        }

        public override void _Process(double delta)
        {
            // FIXME: shouldn't do this every frame, instead have external system run it at desired intervals!
            _rootBehavior.Tick(this);
        }

        // FIXME: adding this function to allow systems to set this, and interface means BT won't have write access
        // Still might be better to avoid this altogether?
        public void SetBlackboard(Blackboard blackboard)
        {
            _blackboard = blackboard;
        }
    }
}