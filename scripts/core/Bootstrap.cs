using Godot;
using System;

public partial class Bootstrap : Node
{
	[Export]
	private PackedScene _startLevel;

	public PackedScene StartLevel => _startLevel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		this.GetCore().Bootstrap(this);
	}
}
