using Godot;
using System;

public partial class SimulationWorld : GodotObject
{
	public int Width;
	public int Height;

	public SimulationWorld(int width, int height)
	{
		Width = width;
		Height = height;
	}
}
