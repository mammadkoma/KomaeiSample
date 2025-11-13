using System;
using System.Collections.Generic;

namespace KomaeiSample.Server.Data.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int EnvelopeId { get; set; }

    public decimal Price { get; set; }

    public int? DiscountId { get; set; }

    public decimal? PriceAfterDiscount { get; set; }

    public int Series { get; set; }

    public int? CellophaneId { get; set; }

    public int? UvId { get; set; }

    public string FileName { get; set; } = null!;

    public string FileExtension { get; set; } = null!;

    public int DeliveryMethodId { get; set; }

    public int? AddressTypeId { get; set; }

    public int? TehranAreaId { get; set; }

    public string? Address { get; set; }

    public string? PostalCode { get; set; }

    public int OrderStatusId { get; set; }

    public string? Desc { get; set; }

    public long? PayRefNumber { get; set; }

    public DateTime? PayDate { get; set; }

    public Guid AddUserId { get; set; }

    public DateTime AddDate { get; set; }

    public Guid? UpdateUserId { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual User AddUser { get; set; } = null!;

    public virtual AddressType? AddressType { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Cellophane? Cellophane { get; set; }

    public virtual DeliveryMethod DeliveryMethod { get; set; } = null!;

    public virtual Discount? Discount { get; set; }

    public virtual OrderStatus OrderStatus { get; set; } = null!;

    public virtual TehranArea? TehranArea { get; set; }

    public virtual User? UpdateUser { get; set; }

    public virtual Uv? Uv { get; set; }
}
