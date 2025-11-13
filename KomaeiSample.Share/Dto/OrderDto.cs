namespace KomaeiSample.Share.Dto;

public class OrderDto
{
    public OrderOfficeDto[]? OrderOfficeDtos { get; set; }
    public OrderHospitalDto[]? OrderHospitalDtos { get; set; }
    public OrderHandBagDto[]? OrderHandBagDtos { get; set; }
    public OrderConfidentialDto[]? OrderConfidentialDtos { get; set; }
    public decimal WalletBalance { get; set; } = 0;
}

public class OrderOfficeDto
{
    public int Id { get; set; }
    public required string ModelTitle { get; set; }
    public required string PaperTitle { get; set; }
    public required string GrammageTitle { get; set; }
    public required string CountTitle { get; set; }
    public required string HasInternalTeramTitle { get; set; }
    public required string HasDoorGlueTitle { get; set; }
    public required decimal Price { get; set; }
    public decimal? PriceAfterDiscount { get; set; }
    public required int Series { get; set; }
    public required string CellophaneTitle { get; set; }
    public required string UvTitle { get; set; }
    public required string DeliveryMethodTitle { get; set; }
    public required string AddressTypeTitle { get; set; }
    public required string TehranAreaTitle { get; set; }
    public required string Address { get; set; }
    public required string PostalCode { get; set; }
    public string? Desc { get; set; }
    public required DateTime AddDate { get; set; }
    public int OrderStatusId { get; set; }
    public string? OrderStatusTitle { get; set; }
    public string? AddUserFullName { get; set; }
    public string? AddUserMobile { get; set; }
    public string FileName { get; set; } = null!;
    public string FileExtension { get; set; } = null!;
    public DateTime? PayDate { get; set; }
    public Guid AddUserId { get; set; }
}

public class OrderHospitalDto
{
    public int Id { get; set; }
    public required string ModelTitle { get; set; }
    public required string HospitalTemplateTemplateName { get; set; }
    public required string PaperTitle { get; set; }
    public required string GrammageTitle { get; set; }
    public required string CountTitle { get; set; }
    public required string CellophaneTitle { get; set; }
    public required string UvTitle { get; set; }
    public required decimal Price { get; set; }
    public decimal? PriceAfterDiscount { get; set; }
    public required string DeliveryMethodTitle { get; set; }
    public required string AddressTypeTitle { get; set; }
    public required string TehranAreaTitle { get; set; }
    public required string Address { get; set; }
    public required string PostalCode { get; set; }
    public string? Desc { get; set; }
    public required DateTime AddDate { get; set; }
    public int OrderStatusId { get; set; }
    public string? OrderStatusTitle { get; set; }
    public string? AddUserFullName { get; set; }
    public string? AddUserMobile { get; set; }
    public string FileName { get; set; } = null!;
    public string FileExtension { get; set; } = null!;
    public DateTime? PayDate { get; set; }
    public Guid AddUserId { get; set; }
}

public class OrderHandBagDto
{
    public int Id { get; set; }
    public required string ModelTitle { get; set; }
    public required string PaperTitle { get; set; }
    public required string GrammageTitle { get; set; }
    public required string CountTitle { get; set; }
    public required string CellophaneTitle { get; set; }
    public required string UvTitle { get; set; }
    public required decimal Price { get; set; }
    public decimal? PriceAfterDiscount { get; set; }
    public required string DeliveryMethodTitle { get; set; }
    public required string AddressTypeTitle { get; set; }
    public required string TehranAreaTitle { get; set; }
    public required string Address { get; set; }
    public required string PostalCode { get; set; }
    public string? Desc { get; set; }
    public required DateTime AddDate { get; set; }
    public int OrderStatusId { get; set; }
    public string? OrderStatusTitle { get; set; }
    public string? AddUserFullName { get; set; }
    public string? AddUserMobile { get; set; }
    public string FileName { get; set; } = null!;
    public string FileExtension { get; set; } = null!;
    public DateTime? PayDate { get; set; }
    public Guid AddUserId { get; set; }
}

public class OrderConfidentialDto
{
    public int Id { get; set; }
    public required string ModelTitle { get; set; }
    public required string PaperTitle { get; set; }
    public required string GrammageTitle { get; set; }
    public required string CountTitle { get; set; }
    public required decimal Price { get; set; }
    public decimal? PriceAfterDiscount { get; set; }
    public required string DeliveryMethodTitle { get; set; }
    public required string AddressTypeTitle { get; set; }
    public required string TehranAreaTitle { get; set; }
    public required string Address { get; set; }
    public required string PostalCode { get; set; }
    public string? Desc { get; set; }
    public required DateTime AddDate { get; set; }
    public int OrderStatusId { get; set; }
    public string? OrderStatusTitle { get; set; }
    public string? AddUserFullName { get; set; }
    public string? AddUserMobile { get; set; }
    public string FileName { get; set; } = null!;
    public string FileExtension { get; set; } = null!;
    public DateTime? PayDate { get; set; }
    public Guid AddUserId { get; set; }
}

public class InvoiceDto
{
    public int? Id { get; set; }
    public string? CategoryTitle { get; set; }
    public string? ModelTitle { get; set; }
    public string? EnvelopeHospitalTemplateName { get; set; }
    public string? PaperTitle { get; set; }
    public string? GrammageTitle { get; set; }
    public string? CountTitle { get; set; }
    public decimal? Price { get; set; }
    public decimal? PriceAfterDiscount { get; set; }
    public DateTime? AddDate { get; set; }
    public string? AddUserFullName { get; set; }
    public string? AddUserMobile { get; set; }
    public DateTime? PayDate { get; set; }
}