using System;
using System.Collections.Generic;

namespace KomaeiSample.Server.Data.Entities;

public partial class HospitalTemplate
{
    public int Id { get; set; }

    public int ModelId { get; set; }

    public string TemplateName { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public short Order { get; set; }

    public virtual ICollection<EnvelopeHospital> EnvelopeHospitals { get; set; } = new List<EnvelopeHospital>();

    public virtual Model Model { get; set; } = null!;
}
