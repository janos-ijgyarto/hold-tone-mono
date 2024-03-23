using BehaviorTree;
using Godot;
using System;

public partial class ComponentList : Node
{
    // FIXME: ensure we are only getting nodes?
    public T GetComponent<[MustBeVariant] T>()
    {
        foreach (Node currentChild in GetChildren())
        {
            if (currentChild is T)
            {
                return (T)(object)currentChild;
            }
        }
        return default;
    }
}
