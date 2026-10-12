using Godot;
using System;

public partial class Simulation : Node2D
{
	private TileMapLayer _mojePlatno;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_mojePlatno = GetNode<TileMapLayer>("TileMapLayer");
		VykresliRaduBunek();
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void VykresliRaduBunek()
	{
	
		
		for (int x = -20; x < 20; x++)
		{
			Vector2I poziceVMrizce = new Vector2I(x, 0);

			// 1. Nejdříve buňku vytvoříme na našem plátně (ID obrázku je 0, pixel na 0,0)
			_mojePlatno.SetCell(poziceVMrizce, 0, Vector2I.Zero);
			
			// 2. Požádáme plátno, aby nám dalo přístup k datům této konkrétní buňky
			TileData dataBunky = _mojePlatno.GetCellTileData(poziceVMrizce);

			// 3. Pokud data v pořádku existují, změníme jejich barvu (Modulate)
			if (dataBunky != null)
			{
				// Nastavíme zářivě oranžovou barvu (R=1.0, G=0.5, B=0.0)
				dataBunky.Modulate = new Color(1.0f, 0.5f, 0.0f);
			}
		}
		
	}
	
}
