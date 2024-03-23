using Godot;
using System;

// Extension to make it easier for nodes to query the core autoload
public static class CoreExtensions
{
	public static Core GetCore(this Node node)
	{
		return node.GetNode<Core>("/root/Core");
	}
}

public partial class Core : Node
{
	public Bootstrap BootstrapData { get; private set; }

	// FIXME: is there a more elegant way to do this?
	public IWorld World => InternalWorld;
	public IGUI GUI => InternalGUI;

	private World InternalWorld => GetNode<World>("World");

    private GUI InternalGUI => GetNode<GUI>("GUI");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		GD.Randomize();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Bootstrap(Bootstrap bootstrap)
	{
		if(BootstrapData != null)
		{
			return;
        }

        BootstrapData = bootstrap;

        InternalGUI.Bootstrap();
        InternalWorld.Bootstrap();
	}
}
