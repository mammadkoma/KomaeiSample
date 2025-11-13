using System;
using System.Collections.Generic;

namespace KomaeiSample.Server.Data.Entities;

public partial class Product
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string Title { get; set; } = null!;

    public decimal Price { get; set; }

    public string FileName { get; set; } = null!;

    public string FileExtension { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;
}
