using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 100.0f;
	public const float Acceleration = 1700.0f;
	public const float Decceleration = 100.0f;

	private Vector2 vel;

	private Sprite2D redSkin;
	private Sprite2D blueSkin;

	[Export] private bool isRed = false;

	private Camera2D cam;

	private Lumball lumball;

	private float shakeTime = 0f;
	private float shakeStrength = 0f;
	private Random rand = new Random();

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

		cam = GetNode<Camera2D>("%Camera2D");

		lumball = GetTree().CurrentScene.GetNode<Lumball>("lumball");
		lumball.LumballTouchedWall += LumballTouchedWall;
	}

	public override void _PhysicsProcess(double delta)
	{
		vel = HandleMovement((float)delta);
		UpdateShake((float)delta);

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

	private void LumballTouchedWall()
	{
		StartShake(0.3f, 1f);
	}

	private void StartShake(float duration, float strength)
	{
		shakeTime = duration;
		shakeStrength = strength;
	}

	private void UpdateShake(float delta)
	{
		if (shakeTime > 0)
		{
			shakeTime -= delta;

			float offsetX = ((float)rand.NextDouble() * 2 - 1) * shakeStrength;
			float offsetY = ((float)rand.NextDouble() * 2 - 1) * shakeStrength;
			cam.Offset = new Vector2(offsetX, offsetY);
		}
		else
		{
			cam.Offset = Vector2.Zero;
		}
	}
}

