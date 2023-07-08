using System.Globalization;
using FluentAssertions;

namespace PricingKata.Tests;



public class PricerShould
{
    [Theory]
    [InlineData(3, 1.21, 0, "3.63 €")]
    [InlineData(3, 1.21, 5, "3.81 €")]
    [InlineData(3, 1.21, 20, "4.36 €")]
    public void Calculate_price(int itemCount, decimal itemPrice, int taxInPercentage, string expected)
        => CalculatePrice(itemCount, itemPrice, taxInPercentage).Should().Be(expected);

    [Theory]
    [InlineData(5, 345, 10, "1840.58 €")]
    [InlineData(5, 1299, 10, "6787.28 €")]
    public void Calculate_price_with_discount_depending_of_the_amount(
        int itemCount,
        decimal itemPrice,
        int taxInPercentage,
        string expected)
        => CalculatePrice(itemCount, itemPrice, taxInPercentage).Should().Be(expected);


    private string CalculatePrice(int itemCount, decimal itemPrice, decimal tax)
    {
        var priceWithoutTax = itemCount * itemPrice;

        var priceWithDiscount = priceWithoutTax - GetDiscountAmount(priceWithoutTax);

        var taxAmount = GetTaxAmount(priceWithDiscount, tax);

        var priceWitTax = priceWithDiscount + taxAmount;

        return Print(priceWitTax);
    }

    private static decimal GetTaxAmount(decimal priceWithDiscount, decimal tax)
        => priceWithDiscount * tax.Percent();

    private static decimal GetDiscountAmount(decimal priceWithoutTax)
    {
        var discountPercentage = priceWithoutTax switch
        {
            > 5000 => 5m,
            > 1000 => 3m,
            _ => 0m
        };

        return priceWithoutTax * discountPercentage / 100;
    }

    private static string Print(decimal price)
        => $"{price.ToString("0.00", CultureInfo.InvariantCulture)} €";
}

public static class MathExtensions
{
    public static decimal Percent(this decimal number)
        => number / 100m;
}