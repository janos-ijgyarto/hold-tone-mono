using Godot;
using System;

// FIXME: this whole thing is really hacky, need a better way to decouple things!
public partial class NPCController : Node
{
	public Vector2 ControlValue {  get; private set; }

	private BehaviorTree.BehaviorTree _behaviorTree;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ComponentList components = GetParent<ComponentList>();
        _behaviorTree = components.GetComponent<BehaviorTree.BehaviorTree>();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        ControlValue = _behaviorTree.Blackboard.Get<Vector2>("control");
    }
}
