using Godot;
using System;

public partial class Simulace : Node2D
{
	
	private SimulationWorld world;
	private double elapsedTime = 0.0;
	private Organismus org1;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		world = new SimulationWorld(100, 100);
		
		org1 = new Organismus();
		org1.Energie = 100;
		org1.Hmotnost = 100;
		org1.Vek = 0;
		
		GD.Print("Experiment zahájen.");
		VypisStav();
		GD.Print("Stiskni MEZERNÍK pro další krok.");
	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		elapsedTime += delta;
	}
	
	public override void _UnhandledKeyInput(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent
			&& keyEvent.Keycode == Key.Space
			&& keyEvent.Pressed
			&& !keyEvent.Echo)
		{
			org1.ZijJednoKolo(1);
			VypisStav();
		}
	}
	
	private void VypisStav()
	{
		GD.Print($"Energie: {org1.Energie}");
		
		GD.Print($"Spotřeba metabolismu: {org1.MiraMetabolismu}");
		
	}

}
