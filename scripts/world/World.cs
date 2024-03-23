using Godot;
using System;

public interface IWorld
{
	public Level CurrentLevel { get; }
}

public partial class World : Node2D, IWorld
{
	private Level _currentLevel = null;

	public Level CurrentLevel => _currentLevel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Bootstrap()
	{
		Core core = this.GetCore();
        Bootstrap bootstrapData = core.BootstrapData;

        if (bootstrapData == null)
		{
			return;
		}

		if(bootstrapData.StartLevel != null)
        {
			LoadLevel(bootstrapData.StartLevel);
        }
	}

	public void LoadLevel(PackedScene level)
	{
		if(_currentLevel != null)
		{
			// Unload previous level
			// TODO: proper level load/unload system!
			RemoveChild(_currentLevel);
			_currentLevel.QueueFree();
		}

		_currentLevel = level.Instantiate<Level>();
		AddChild(_currentLevel);
	}
}
