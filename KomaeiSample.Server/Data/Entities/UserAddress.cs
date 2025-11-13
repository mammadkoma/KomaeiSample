using System;
using System.Collections.Generic;

namespace KomaeiSample.Server.Data.Entities;

public partial class UserAddress
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
