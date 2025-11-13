using KomaeiSample.Share.Dto;

namespace KomaeiSample.Share.Common;
public static class DecimalExtensions
{
    public static decimal CalculatePriceAfterDiscount(this decimal? price, DiscountDto discountDto)
    {
        var discount = price * discountDto.Percent / 100;
        if (discount > discountDto.MaxPrice)
            discount = discountDto.MaxPrice;
        return price!.Value - discount!.Value;
    }

    public static decimal CalculatePriceAfterDiscount(this decimal price, DiscountDto discountDto)
    {
        var discount = price * discountDto.Percent / 100;
        if (discount > discountDto.MaxPrice)
            discount = discountDto.MaxPrice;
        return price - discount;
    }
}