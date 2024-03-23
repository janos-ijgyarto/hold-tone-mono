using Godot;
using System;

public partial class Level : Node2D
{
	[Export]
	private PackedScene _playerPrefab;

	[Export]
	private PackedScene _npcPrefab;

	private Node2D _entities;
	private Camera2D _camera;

	private Player _player;
	private Godot.Collections.Array<BehaviorTree.Blackboard> _npcBlackboards = new Godot.Collections.Array<BehaviorTree.Blackboard>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		_entities = GetNode<Node2D>("Entities");

		_player = _playerPrefab.Instantiate<Player>();
		_player.Position = Vector2.Zero;

		_entities.AddChild(_player);

		Node2D npcsNode = _entities.GetNode<Node2D>("NPCs");

        NPC spawnedNPC = _npcPrefab.Instantiate<NPC>();

        spawnedNPC.Position = new Vector2(GD.RandRange(-200, 200), GD.RandRange(-200, 200));

		npcsNode.AddChild(spawnedNPC);

		// Add a blackboard to each NPC
		foreach (Node currentChild in npcsNode.GetChildren())
		{
			if(currentChild is NPC)
			{
				NPC currentNPC = currentChild as NPC;
				BehaviorTree.BehaviorTree npcBehaviorTree = currentNPC.Components.GetComponent<BehaviorTree.BehaviorTree>();

				BehaviorTree.Blackboard blackboard = new BehaviorTree.Blackboard();
                npcBehaviorTree.SetBlackboard(blackboard);

				// FIXME: this is a hack, we need to make sure to have an initialization step for the NPC BehaviorTree
                blackboard.Set("control", Vector2.Zero);

                _npcBlackboards.Add(blackboard);
            }
		}

        _camera = GetNode<Camera2D>("Camera2D");
	}

	public override void _Process(double delta)
	{
		_camera.Position = _player.Position;

        Node2D npcsNode = _entities.GetNode<Node2D>("NPCs");

		// Update all blackboards
        for (int currentNPCIndex = 0; currentNPCIndex < _npcBlackboards.Count; ++currentNPCIndex)
		{
			BehaviorTree.Blackboard currentNPCBlackboard = _npcBlackboards[currentNPCIndex];
			currentNPCBlackboard.SetData("player", _player.GlobalPosition);

			Vector2 currentNPCPosition = npcsNode.GetChild<Node2D>(currentNPCIndex).GlobalPosition;

			currentNPCBlackboard.SetData("position", currentNPCPosition);
        }
    }
}
