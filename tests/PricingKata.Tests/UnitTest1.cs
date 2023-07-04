namespace PricingKata.Tests;

public class UnitTest1
{
    [Theory]
    [InlineData(3, 1.21, 0, "3.63 €")]
    [InlineData(3, 1.21, 5, "3.81 €")]
    [InlineData(3, 1.21, 20, "4.36 €")]
    public void Test1(int itemCount, decimal itemPrice, int taxRateInPercentage, string expectedPrice)
        => GetPrice(itemCount, itemPrice, taxRateInPercentage).Should().Be(expectedPrice);

    private string GetPrice(int itemCount, decimal itemPrice, int taxRateInPercentage)
    {
        var priceWithoutTax = itemCount * itemPrice;
        var priceWithTax = ApplyTax(priceWithoutTax, taxRateInPercentage);
        return ToString(priceWithTax);
    }

    private static decimal ApplyTax(decimal priceWithoutTax, int taxRateInPercentage)
        => priceWithoutTax * ConvertTaxRate(taxRateInPercentage);

    private static decimal ConvertTaxRate(decimal taxRateInPercentage)
        => taxRateInPercentage / 100m + 1;

    private static string ToString(decimal price)
        => FormattableString.Invariant($"{price:0.00} €");
}