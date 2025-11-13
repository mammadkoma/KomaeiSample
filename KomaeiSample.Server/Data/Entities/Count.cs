using System;
using System.Collections.Generic;

namespace KomaeiSample.Server.Data.Entities;

public partial class Count
{
    public int Id { get; set; }

    public int Title { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<EnvelopeConfidential> EnvelopeConfidentials { get; set; } = new List<EnvelopeConfidential>();

    public virtual ICollection<EnvelopeHandBag> EnvelopeHandBags { get; set; } = new List<EnvelopeHandBag>();

    public virtual ICollection<EnvelopeHospital> EnvelopeHospitals { get; set; } = new List<EnvelopeHospital>();

    public virtual ICollection<EnvelopeOffice> EnvelopeOffices { get; set; } = new List<EnvelopeOffice>();
}
