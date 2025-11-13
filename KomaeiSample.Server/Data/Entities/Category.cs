using System;
using System.Collections.Generic;

namespace KomaeiSample.Server.Data.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? FileName { get; set; }

    public string? FileExtension { get; set; }

    public string? Desc { get; set; }

    public virtual ICollection<Count> Counts { get; set; } = new List<Count>();

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual ICollection<Grammage> Grammages { get; set; } = new List<Grammage>();

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Paper> Papers { get; set; } = new List<Paper>();
}
