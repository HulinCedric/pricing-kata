using FluentAssertions;

namespace PricingKata.Tests;

public class PricerShould
{
    [Theory]
    [InlineData(1, 1, 0, "1.00 €")]
    [InlineData(3, 1.21, 0, "3.63 €")]
    [InlineData(3, 1.21, 5, "3.81 €")]
    [InlineData(3, 1.21, 20, "4.36 €")]
    public void Calculate_price_with_tax(int itemCount, decimal itemUnitPrice, int tax, string expected)
        => CalculatePrice(itemCount, itemUnitPrice, tax).Should().Be(expected);

    [Theory]
    [InlineData(5, 345, 10, "1840.58 €")]
    [InlineData(5, 1299, 10, "6787.28 €")]
    public void Discount_based_on_price_amount(int itemCount, decimal itemUnitPrice, int tax, string expected)
        => CalculatePrice(itemCount, itemUnitPrice, tax).Should().Be(expected);

    private string CalculatePrice(int itemCount, decimal itemUnitPrice, int tax)
    {
        var priceWithoutTax = TaxPriceWithoutTax(itemCount, itemUnitPrice);

        var priceWithDiscount = priceWithoutTax - DiscountAmount(priceWithoutTax);

        var priceWithTax = priceWithDiscount + TaxAmount(priceWithDiscount, tax);

        return FormattableString.Invariant($"{priceWithTax:0.00} €");
    }

    private static decimal TaxPriceWithoutTax(int itemCount, decimal itemUnitPrice)
        => itemCount * itemUnitPrice;

    private static decimal DiscountAmount(decimal priceWithoutTax)
    {
        var discount = priceWithoutTax switch
        {
            > 5000 => 5m,
            > 1000 => 3m,
            _ => 0m
        };

        return priceWithoutTax * discount / 100m;
    }

    private static decimal TaxAmount(decimal priceWithDiscount, int tax)
        => priceWithDiscount * tax / 100;
}