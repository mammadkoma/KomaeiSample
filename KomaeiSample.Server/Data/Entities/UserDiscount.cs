using System;
using System.Collections.Generic;

namespace KomaeiSample.Server.Data.Entities;

public partial class UserDiscount
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public int DiscountId { get; set; }

    public virtual Discount Discount { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
