using Godot;
using System;

public partial class PlayerController : Node
{
	public Vector2 ControlValue { get; private set; }

	public override void _Process(double delta)
	{
		ControlValue = Input.GetVector("move_left", "move_right", "move_up", "move_down");
    }
}
