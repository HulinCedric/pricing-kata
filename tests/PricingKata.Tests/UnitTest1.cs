using FluentAssertions;

namespace PricingKata.Tests;

public class PricerShould
{
    [Theory]
    [InlineData(3, 1.21, 0, "3.63 €")]
    [InlineData(3, 1.21, 5, "3.81 €")]
    [InlineData(3, 1.21, 20, "4.36 €")]
    [InlineData(5, 345, 10, "1840.58 €")]
    [InlineData(5, 1299, 10, "6787.28 €")]
    public void Calculate_price(int itemCount, decimal itemPrice, decimal tax, string expected)
        => Pricer.CalculatePrice(itemCount, itemPrice, tax).Should().Be(expected);
}