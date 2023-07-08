namespace PricingKata.Tests;

public static class Pricer
{
    private static readonly (decimal Thresold, decimal Discount)[] DiscountGrid;

    static Pricer()
        => DiscountGrid = new[]
        {
            (Thresold: 5000m, Discount: 0.05m),
            (Thresold: 1000m, Discount: 0.03m),
            (Thresold: 0m, Discount: 0m)
        };

    public static string CalculatePrice(int itemCount, decimal itemPrice, decimal tax)
    {
        var priceWithoutTax = PriceWithoutTax(itemCount, itemPrice);

        var priceWithDiscount = priceWithoutTax - DiscountAmount(priceWithoutTax);

        var priceWithTax = priceWithDiscount + TaxAmount(priceWithDiscount, tax);

        return PrintInEuro(priceWithTax);
    }

    private static decimal PriceWithoutTax(int itemCount, decimal itemPrice)
        => itemCount * itemPrice;

    private static decimal DiscountAmount(decimal priceWithoutTax)
    {
        var discount = DiscountGrid.First(d => priceWithoutTax > d.Thresold).Discount;

        return priceWithoutTax * discount;
    }

    private static decimal TaxAmount(decimal priceWithoutTax, decimal tax)
        => priceWithoutTax * tax / 100;

    private static string PrintInEuro(decimal priceWithTax)
        => FormattableString.Invariant($"{priceWithTax:0.00} €");
}