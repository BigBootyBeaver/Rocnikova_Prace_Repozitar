using Godot;
using System;

public partial class Organismus : GodotObject
{
	// --- Pozice na mřížce ---
	public int X { get; set; }
	public int Y { get; set; }
	
	// --- Aktuální stav (mění se každé kolo) ---
	public double Energie { get; set; } // Peníze v hotovosti (ATP)
	public double Hmotnost { get; set; } // Velikost těla. Když dosáhne limitu, organismus se rozdělí.
	public int Vek { get; set; } 
	
	// --- GENETICKÝ KÓD (Evoluční vlastnosti - dědí se a mutují) ---
	public double GenMiraFotosyntezy { get; set; } = 1;      // Jak moc efektivně mění světlo na hmotu/energii
	public double GenMiraAbsorbce { get; set; } = 0;            // Jak efektivně tráví ostatní políčka (dravost)
	public double GenRychlostPohybu { get; set; } = 0;         // Šance/frekvence pohybu na sousední políčko
	public int GenMaxVek { get; set; } = 100;       // Přirozená délka života
	
	public double MiraMetabolismu // Fixní cena za existenci (bazální metabolismus)
	{ 
		get 
		{
			// 1. Fixní základní cena za to, že buňka vůbec žije
			double zaklad = 0.05; 
			// 2. Daň za fotosyntézu (čím výkonnější chloroplasty, tím dražší údržba)
			double danZaFotosyntezu = GenMiraFotosyntezy * 0.2; 
			// 3. Daň za dravost (udržování receptorů, enzymů, zbraní)
			double danZaAbsorbci = GenMiraAbsorbce * 0.5; // Lov je drahý, proto vyšší násobitel
			// 4. Daň za POHYBOVÝ APARÁT
			double danZaPohybovyAparat = GenRychlostPohybu * 0.6; // Vysoký násobitel, rychlý motor je drahý   // I když organismus zrovna stojí, udržovat bičíky a rychlé buněčné dýchání stojí energii.
			// 5. Daň za velikost (větší tělo spotřebuje víc energie na údržbu)
			double danZaHmotnost = Hmotnost * 0.01; 

			return zaklad + danZaFotosyntezu + danZaAbsorbci + danZaPohybovyAparat + danZaHmotnost;
		}
		
		set {}
	} 

}

public partial class Organismus :GodotObject 
{
	// --- METODY PRO SIMULACI ---
	/// <summary>
	/// Řasa nasává světlo z okolí a dělá z něj novou energii.
	/// </summary>
	public void Fotosyntetizuj(double intenzitaSvetla)
	{
		// Světlo se mění na fyzickou hmotnost těla
		double ziskanaEnergie = intenzitaSvetla * GenMiraFotosyntezy * 0.5;
		Energie += ziskanaEnergie;
		GD.Print($"Zisk fotosyntezy: {ziskanaEnergie}");
	}

	/// <summary>
	/// Volá se každé herní kolo (tick). Organismus zaplatí daň za život a zestárne.
	/// </summary>
	public void ZijJednoKolo(double intenzitaSvetla) // poznámka bacha na pojmenování inntenzita světla pro přehlednost (je to na víc místech jako vnitřní parametr funkce).
	{
		Vek++;
		Fotosyntetizuj(intenzitaSvetla);
		Energie -= MiraMetabolismu;
		
	}

	/// <summary>
	/// Pokud se organismus v tomto kole rozhodne pohnout, zavolá se tato metoda.
	/// </summary>
	public void PohniSe(int noveX, int noveY)
	{
		X = noveX;
		Y = noveY;
		// AKTIVNÍ VÝDAJ: Skutečný pohyb spotřebuje extra balík energie (ATP) navíc!
		// Čím těžší organismus je, tím víc energie ho pohyb stojí (setrvačnost).
		double cenaZaPohyb = 0.3 + (Hmotnost * 0.05);
		Energie -= cenaZaPohyb;
	}

}
