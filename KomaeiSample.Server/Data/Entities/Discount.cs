using System;
using System.Collections.Generic;

namespace KomaeiSample.Server.Data.Entities;

public partial class Discount
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string Code { get; set; } = null!;

    public byte Percent { get; set; }

    public decimal MaxPrice { get; set; }

    public bool Disable { get; set; }

    public Guid AddUserId { get; set; }

    public DateTime AddDate { get; set; }

    public Guid? UpdateUserId { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual User AddUser { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User? UpdateUser { get; set; }

    public virtual ICollection<UserDiscount> UserDiscounts { get; set; } = new List<UserDiscount>();
}
