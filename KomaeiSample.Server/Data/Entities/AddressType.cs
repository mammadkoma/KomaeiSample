using System;
using System.Collections.Generic;

namespace KomaeiSample.Server.Data.Entities;

public partial class AddressType
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
