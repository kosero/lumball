using Godot;
using System;

public partial class Goal : Area2D
{
	[Export] public bool isRed = false;

	private Sprite2D redSkin;
	private Sprite2D blueSkin;

	[Signal] public delegate void LumballInGoalEventHandler();

	public override void _Ready()
	{
		redSkin = GetNode<Sprite2D>("Skins/Red");
		blueSkin = GetNode<Sprite2D>("Skins/Blue");

		if (isRed)
		{
			redSkin.Visible = true;
			blueSkin.Visible = false;
		}
		else
		{
			redSkin.Visible = false;
			blueSkin.Visible = true;
		}

		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node body)
	{
		if (body is TileMapLayer)
			return;

		if (body is Lumball)
		{
			body.QueueFree();
			EmitSignal("LumballInGoal");
		}
	}

}
