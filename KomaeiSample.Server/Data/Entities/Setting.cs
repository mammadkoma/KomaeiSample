using System;
using System.Collections.Generic;

namespace KomaeiSample.Server.Data.Entities;

public partial class Setting
{
    public int Id { get; set; }

    public string Key { get; set; } = null!;

    public string KeyFa { get; set; } = null!;

    public string Value { get; set; } = null!;

    public byte Type { get; set; }
}
