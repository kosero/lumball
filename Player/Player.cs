using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 180.0f;

	public const float Acceleration = 500.0f;
	public const float Decceleration = 100.0f;

	private Vector2 vel;

	public override void _PhysicsProcess(double delta)
	{
		vel = HandleMovement((float)delta);

		Velocity = vel;

		MoveAndSlide();

		Position = new Vector2(Mathf.Round(Position.X), Mathf.Round(Position.Y));
	}

	private Vector2 HandleMovement(float delta)
	{
		Vector2 direction = Input.GetVector("player_left", "player_right", "player_up", "player_down");

		if (direction != Vector2.Zero)
			vel = vel.MoveToward(direction.Normalized() * Speed, Acceleration * delta);
		else
			vel = vel.MoveToward(Vector2.Zero, Decceleration * delta);

		return vel;
	}
}

