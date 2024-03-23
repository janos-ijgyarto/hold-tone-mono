using Godot;
using System;

[GlobalClass]
public partial class PlayerConfig : Resource
{
    [Export]
    public float speed = 1.0f;

    [Export]
    public float acceleration = 1.0f;
}
