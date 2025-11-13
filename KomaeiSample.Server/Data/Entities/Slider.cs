using System;
using System.Collections.Generic;

namespace KomaeiSample.Server.Data.Entities;

public partial class Slider
{
    public int Id { get; set; }

    public string FileName { get; set; } = null!;

    public short Order { get; set; }

    public string? Link { get; set; }
}
