using Godot;
using System;

public partial class MainMenu : Control
{
	private Button debugbtn;

	public override void _Ready()
	{
		debugbtn = GetNode<Button>("%debugBtn");
		debugbtn.Pressed += debugPressed;

	}

	private void debugPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Debug/debug.tscn");
	}
}
