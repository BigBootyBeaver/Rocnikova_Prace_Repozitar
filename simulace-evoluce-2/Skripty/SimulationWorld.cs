using Godot;
using System;

public partial class SimulationWorld : GodotObject
{
	public int Width { get; private set; }
	public int Height { get; private set; }
	public double GlobalLightIntensity { get; private set; }
	private double[,] lightGrid;
	
	public SimulationWorld(int width, int height, double globalLightIntensity)
	{
		Width = width;
		Height = height;
		GlobalLightIntensity = globalLightIntensity;
		lightGrid = new double[width, height];
		// Naplníme celou mřížku výchozí hodnotou světla
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				lightGrid[x, y] = globalLightIntensity;
			}
		}
		
		
	}
	public double GetLightIntensity(int x, int y)
	{
		// Pojistka: pokud by organismus vylezl z mřížky, vrátíme 0 (tmu) místo pádu programu
		if (x < 0 || x >= Width || y < 0 || y >= Height)
		{
			return 0.0;
		}
		return lightGrid[x, y];
	}

}	
