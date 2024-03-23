using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	private PlayerConfig _config;

	private ComponentList _components;

	private PlayerController _controller;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
		_components = GetNode<ComponentList>("Components");
        _controller = _components.GetComponent<PlayerController>();
	}

    public override void _PhysicsProcess(double delta)
    {
		Vector2 prevVelocity = Velocity;
		Vector2 newVelocity = _controller.ControlValue * _config.speed;

		Velocity = prevVelocity.Lerp(newVelocity, (float)delta * _config.acceleration);
		MoveAndSlide();
    }
}
