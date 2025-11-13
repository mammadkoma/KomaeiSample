using System;
using System.Collections.Generic;

namespace KomaeiSample.Server.Data.Entities;

public partial class Cellophane
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<EnvelopeHandBag> EnvelopeHandBags { get; set; } = new List<EnvelopeHandBag>();

    public virtual ICollection<EnvelopeHospital> EnvelopeHospitals { get; set; } = new List<EnvelopeHospital>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
