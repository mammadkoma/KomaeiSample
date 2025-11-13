namespace KomaeiSample.Share.Vm;
public class OrderVm
{
    public int CategoryId { get; set; } = 0;

    public int? EnvelopeId { get; set; }

    public decimal? Price { get; set; }

    [MaxLength(20, ErrorMessage = Constants.MaxLengthMsg)]
    public string? DiscountCode { get; set; } = null!;

    public int? DiscountId { get; set; }

    public int? Series { get; set; } = 1;

    public int? DeliveryMethodId { get; set; }

    public int? AddressTypeId { get; set; }

    public int? TehranAreaId { get; set; }

    public string? Address { get; set; } = null!;

    [MaxLength(100, ErrorMessage = Constants.MaxLengthMsg)]
    public string? Desc { get; set; } = null!;

    public string? PostalCode { get; set; } = null!;

    public int? ModelId { get; set; }

    public int? PaperId { get; set; }

    public int? GrammageId { get; set; }

    public int? CountId { get; set; }

    public byte? HasInternalTeramId { get; set; }

    public byte? HasDoorGlueId { get; set; }

    public int? CellophaneId { get; set; }

    public int? UvId { get; set; }

    public IBrowserFile? File { get; set; }

    public int? HospitalTemplateId { get; set; }
}

public class OrderEditVm
{
    public int Id { get; set; }
    public int OrderStatusId { get; set; }
    public string? AddUserMobile { get; set; }
}