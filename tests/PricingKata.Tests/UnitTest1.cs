namespace PricingKata.Tests;

public class UnitTest1
{
    // 3 articles à 1,21 € et taxe 0 % → “3.63 €”
    [Fact]
    public void Test1()
    {
        GetPrice(3, 1.21m, 0).Should().Be("3.63 €");
    }
    
    // 3 articles à 1,21 € et taxe 5 % → “3.81 €”
    [Fact]
    public void Test2()
    {
        GetPrice(3, 1.21m, 5).Should().Be("3.81 €");
    }
    
    // 3 articles à 1,21 € et taxe 20 % → “4.36 €”
    [Fact]
    public void Test3()
    {
        GetPrice(3, 1.21m, 20).Should().Be("4.36 €");
    }

    private string GetPrice(int itemCount, decimal itemPrice, int taxRateInPercentage)
    {
        var priceWithoutTax = itemCount * itemPrice;
        var price = ApplyTax(taxRateInPercentage, priceWithoutTax);
        return ToString(price);
    }

    private static decimal ApplyTax(int taxRateInPercentage, decimal priceWithoutTax)
    {
        var price = taxRateInPercentage switch
        {
            20 => priceWithoutTax * ConvertTaxRate(20),
            5 => priceWithoutTax * ConvertTaxRate(5),
            0 => priceWithoutTax * ConvertTaxRate(0),
        };
        return price;
    }

    private static decimal ConvertTaxRate(decimal taxRateInPercentage)
        => taxRateInPercentage / 100m + 1;

    private static string ToString(decimal price)
    {
        return FormattableString.Invariant($"{price:0.00} €");
    }
}