using Godot;

public interface IGUI
{
	// TODO!!!
}

public partial class GUI : Control, IGUI
{
	// FIXME: make this more modular?
	[Export]
	private PackedScene _mainMenuScene;

	private MainMenu _mainMenu;

	private Node _currentUIScene = null; // FIXME: allow multiple "UI layers" to be active at a time?

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

        if (bootstrapData.StartLevel == null)
        {
            LoadMainMenu();
        }
    }

	private void LoadUIScene(Node uiSceneNode)
	{
        CallDeferred(MethodName.DeferredLoadUIScene, uiSceneNode);
    }

	private void DeferredLoadUIScene(Node uiSceneNode)
	{
		if(_currentUIScene != null)
		{
			RemoveChild(_currentUIScene);
		}

        if (uiSceneNode != null)
		{
            AddChild(uiSceneNode);
        }
        _currentUIScene = uiSceneNode;
    }

	private void LoadMainMenu()
	{
		_mainMenu = _mainMenuScene.Instantiate<MainMenu>();
		LoadUIScene(_mainMenu);
    }
}
