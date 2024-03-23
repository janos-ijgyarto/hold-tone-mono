using Godot;
using System;

public partial class NPC : Node2D
{
    [Export]
    private NPCConfig _config;

    private ComponentList _components;
    private NPCController _controller;

    public ComponentList Components { get { return _components; } }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		_components = GetNode<ComponentList>("Components");
        _controller = _components.GetComponent<NPCController>();
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = _controller.ControlValue * _config.speed;
        Position += velocity * (float)delta;
    }
}
