using System;
using System.Collections.Generic;

namespace Bee_Healthy_Backend.Models;

public partial class GyogyszerAdatok
{
    public int Id { get; set; }

    public string GyogyszerNev { get; set; } = null!;

    public string Marka { get; set; } = null!;

    public string Kategoria { get; set; } = null!;

    public string Adagolas { get; set; } = null!;

    public string KezelesiIdopont { get; set; } = null!;

    public string KezelesIdotartama { get; set; } = null!;

    public DateTime Emlekezteto { get; set; }

    public string Megjegyzes { get; set; } = null!;
}
