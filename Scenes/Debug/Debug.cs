using Godot;
using System;

public partial class Debug : Node2D
{
	private PackedScene lumball;
	private PackedScene player;

	private Goal redGoal;
	private Goal blueGoal;

	private Control scoreAndTimer;
	private Label redTeamStatus;
	private Label blueTeamStatus;

	private Label matchTimer;

	private int redScore = 00;
	private int blueScore = 00;

	private float elapsedTime = 0f;

	[Signal]
	public delegate void GoalRedEventHandler();
	[Signal]
	public delegate void GoalBlueEventHandler();

	private Node2D lumballInstance;

	public override void _Ready()
	{
		lumball = GD.Load<PackedScene>("res://lumball/lumball.tscn");
		player = GD.Load<PackedScene>("res://Player/player.tscn");

		redGoal = GetNode<Goal>("%RedGoal");
		blueGoal = GetNode<Goal>("%BlueGoal");

		redGoal.LumballInGoal += redGoalInLumball;
		blueGoal.LumballInGoal += blueGoalInLumball;

		scoreAndTimer = GetNode<Control>("%ScoreAndTimer");
		redTeamStatus = scoreAndTimer.GetNode<Label>("%redTeamStatus");
		blueTeamStatus = scoreAndTimer.GetNode<Label>("%blueTeamStatus");
		matchTimer = scoreAndTimer.GetNode<Label>("%MatchTimer");

		redTeamStatus.Text = redScore.ToString();
		blueTeamStatus.Text = blueScore.ToString();

		SpawnLumball();
		SpawnPlayer();
	}

	public override void _Process(double delta)
	{
		elapsedTime += (float)delta;

		int minutes = (int)(elapsedTime / 60);
		int seconds = (int)(elapsedTime % 60);
		matchTimer.Text = $"{minutes:D2}:{seconds:D2}";
	}

	private void SpawnLumball()
	{
		if (lumball != null)
		{
			lumballInstance = (Node2D)lumball.Instantiate();
			lumballInstance.Position = new Vector2(240, 144);
			AddChild(lumballInstance);
		}
	}

	//	This is just example, not really spawner
	private void SpawnPlayer()
	{
		if (lumball != null)
		{
			Node2D playerInstance = (Node2D)player.Instantiate();
			playerInstance.Position = new Vector2(300, 144);
			AddChild(playerInstance);
		}

	}

	private void ResetLumball()
	{
		lumballInstance.Position = new Vector2(240, 144);

		var body = lumballInstance as RigidBody2D;
		if (body != null)
		{
			body.LinearVelocity = Vector2.Zero;
			body.AngularVelocity = 0;
			body.Sleeping = false;
		}
	}

	private void redGoalInLumball()
	{
		blueScore++;
		blueTeamStatus.Text = blueScore.ToString();

		CallDeferred(nameof(ResetLumball));
		EmitSignal("GoalRed");
	}

	private void blueGoalInLumball()
	{
		redScore++;
		redTeamStatus.Text = redScore.ToString();

		CallDeferred(nameof(ResetLumball));
		EmitSignal("GoalBlue");
	}
}
