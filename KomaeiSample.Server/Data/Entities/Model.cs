using System;
using System.Collections.Generic;

namespace KomaeiSample.Server.Data.Entities;

public partial class Model
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int CategoryId { get; set; }

    public string? FileName { get; set; }

    public string? FileExtension { get; set; }

    public string? Desc { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<EnvelopeConfidential> EnvelopeConfidentials { get; set; } = new List<EnvelopeConfidential>();

    public virtual ICollection<EnvelopeHandBag> EnvelopeHandBags { get; set; } = new List<EnvelopeHandBag>();

    public virtual ICollection<EnvelopeHospital> EnvelopeHospitals { get; set; } = new List<EnvelopeHospital>();

    public virtual ICollection<EnvelopeOffice> EnvelopeOffices { get; set; } = new List<EnvelopeOffice>();

    public virtual ICollection<HospitalTemplate> HospitalTemplates { get; set; } = new List<HospitalTemplate>();
}
