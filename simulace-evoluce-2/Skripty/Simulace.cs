using Godot;
using System;
using System.Collections.Generic; // aby to mohlo používat List atd.

public partial class Simulace : Node2D
{
	
	private SimulationWorld world;
	private double elapsedTime = 0.0;
	private const int SirkaSveta = 50;
	private const int VyskaSveta = 50;
	private const double GlobalniIntenzitaSvetla = 1;
	
	private List<Organismus> seznamOrganismu = new List<Organismus>();
	
	private Organismus org1;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		world = new SimulationWorld(SirkaSveta, VyskaSveta, GlobalniIntenzitaSvetla);
		
		for (int i = 0; i < 3; i++) 
		{
			Organismus org = new Organismus();
			org.X = 10 + i;
			org.Y = 25;
			org.Energie = 50 + (i * 30);
			org.Hmotnost = 50;
			org.Vek = 0;
			
			org.GenMaxVek = 80 + (i * 20);
			seznamOrganismu.Add(org);
		}
		
		
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
			// Procházíme seznam ODZADU (od nejvyššího indexu po nulu).
			// Důvod: Pokud bychom šli odpředu a nějaký organismus smazali, 
			// posunuly by se indexy a program by přeskočil následující organismus nebo spadl.
			for (int i = seznamOrganismu.Count - 1; i >= 0; i--)
			{
				Organismus org = seznamOrganismu[i];

				// 1. Organismus odehraje své kolo
				org.ZijJednoKolo(world);

				// 2. Kontrola smrti: Pokud v tomto kole zemřel, smažeme ho ze světa
				if (org.JeMrtvy)
				{
					GD.Print($"[-] Organismus na [{org.X}, {org.Y}] zemřel (Věk: {org.Vek}, Energie: {org.Energie}).");
					seznamOrganismu.RemoveAt(i); // Odstranění ze simulace
				}
			}
			VypisStav();
		}
	}
	
	private void VypisStav()
	{
		GD.Print($"=== AKTUÁLNÍ STAV (Počet živých: {seznamOrganismu.Count}) ===");
	
		// Projdeme seznam od začátku do konce a vypíšeme detaily pro každý organismus
		for (int i = 0; i < seznamOrganismu.Count; i++)
		{
			Organismus org = seznamOrganismu[i];
			
			// Výpis pro jeden konkrétní organismus
			GD.Print($"Organismus #{i} na pozici [{org.X}, {org.Y}]:");
			GD.Print($"  -> Energie: {org.Energie:F1}");
			GD.Print($"  -> Spotřeba metabolismu: {org.MiraMetabolismu:F2}");
			GD.Print($"  -> Věk: {org.Vek}/{org.GenMaxVek} | Hmotnost: {org.Hmotnost:F1}");
		}
		
		GD.Print("--------------------------------------------------");
			
	}

}
