using Godot;
using System;

public partial class Lumball : RigidBody2D
{
	private AnimatedSprite2D anim;
	private Area2D area;

	public override void _Ready()
	{
		anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		anim.Play("default");

		area = GetNode<Area2D>("%Area2D");
		area.BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node body)
	{
		if (body is TileMapLayer)
			GD.Print("TODO");
	}
}
