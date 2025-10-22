using Godot;
using System;

public partial class Debug : Node2D
{
	private PackedScene lumball;

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

	public override void _Ready()
	{
		lumball = GD.Load<PackedScene>("res://lumball/lumball.tscn");

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
			Node2D lumballInstance = (Node2D)lumball.Instantiate();
			lumballInstance.Position = new Vector2(240, 144);
			lumballInstance.Name = "Lumball_" + GD.Randi();
			AddChild(lumballInstance);
		}
	}

	private void redGoalInLumball()
	{
		redScore++;
		redTeamStatus.Text = redScore.ToString();

		EmitSignal("GoalRed");
		CallDeferred(nameof(SpawnLumball));
	}

	private void blueGoalInLumball()
	{
		blueScore++;
		blueTeamStatus.Text = blueScore.ToString();

		CallDeferred(nameof(SpawnLumball));
		EmitSignal("GoalBlue");
	}
}
