namespace PricingKata.Tests;

public class Cart
{
    public static string GetTotal(int itemCount, decimal itemPrice, int taxRateInPercentage)
    {
        var priceWithoutTax = itemCount * itemPrice;

        var priceWithDiscount = priceWithoutTax - GetDiscountAmount(priceWithoutTax);

        var priceWithTax = ApplyTax(priceWithDiscount, taxRateInPercentage);

        return ToString(priceWithTax);
    }

    private static decimal GetDiscountAmount(decimal priceWithoutTax)
    {
        var discountRateInPercentage =
            priceWithoutTax > 5000 ? 5m :
            priceWithoutTax > 1000 ? 3m :
            0m;

        var discountRate = discountRateInPercentage / 100m;

        return priceWithoutTax * discountRate;
    }

    private static decimal ApplyTax(decimal priceWithoutTax, int taxRateInPercentage)
        => priceWithoutTax * ConvertTaxRate(taxRateInPercentage);

    private static decimal ConvertTaxRate(decimal taxRateInPercentage)
        => taxRateInPercentage / 100m + 1;

    private static string ToString(decimal price)
        => FormattableString.Invariant($"{price:0.00} €");
}

public class CarteShould
{
    [Theory]
    [InlineData(3, 1.21, 0, "3.63 €")]
    [InlineData(3, 1.21, 5, "3.81 €")]
    [InlineData(3, 1.21, 20, "4.36 €")]
    public void GetTotal(int itemCount, decimal itemPrice, int taxRateInPercentage, string expectedPrice)
        => Cart.GetTotal(itemCount, itemPrice, taxRateInPercentage).Should().Be(expectedPrice);

    [Theory]
    [InlineData(5, 345, 10, "1840.58 €")]
    public void Get_total_with_3_percent_discount_when_total_price_without_tax_over_1000(
        int itemCount,
        decimal itemPrice,
        int taxRateInPercentage,
        string expectedPrice)
        => Cart.GetTotal(itemCount, itemPrice, taxRateInPercentage).Should().Be(expectedPrice);


    [Theory]
    [InlineData(5, 1299, 10, "6787.28 €")]
    public void Get_total_with_5_percent_discount_when_total_price_without_tax_over_5000(
        int itemCount,
        decimal itemPrice,
        int taxRateInPercentage,
        string expectedPrice)
        => Cart.GetTotal(itemCount, itemPrice, taxRateInPercentage).Should().Be(expectedPrice);
}