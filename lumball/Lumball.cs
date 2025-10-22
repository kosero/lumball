using Godot;

public partial class Lumball : RigidBody2D
{
	private AnimatedSprite2D anim;
	private Area2D area;

	[Signal] public delegate void LumballTouchedWallEventHandler();
	[Signal] public delegate void PlayerTouchedLumballEventHandler();

	public override void _Ready()
	{
		anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		anim.Play("default");

		area = GetNode<Area2D>("%Area2D");
		area.BodyEntered += OnBodyEntered;
	}

	private async void OnBodyEntered(Node body)
	{
		if (body is TileMapLayer)
		{
			EmitSignal("LumballTouchedWall");
			return;
		}

		if (body is Player)
		{
			EmitSignal("PlayerTouchedLumball");
			return;
		}
	}
}
