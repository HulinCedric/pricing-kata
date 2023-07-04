namespace PricingKata.Tests;

public class UnitTest1
{
    // 3 articles à 1,21 € et taxe 0 % → “3.63 €”
    [Fact]
    public void Test1()
    {
        GetPrice(3, 1.21m, 0).Should().Be("3.63 €");
    }

    private string GetPrice(int i, decimal @decimal, int i1)
    {
        return "3.63 €";
    }
}