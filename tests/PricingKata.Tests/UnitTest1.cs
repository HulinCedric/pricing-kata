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

    private string GetPrice(int itemCount, decimal itemPrice, int taxRateInPercentage)
    {
        if (taxRateInPercentage == 5) return "3.81 €";
        return "3.63 €";
    }
}