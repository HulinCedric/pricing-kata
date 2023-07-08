using System.Globalization;

namespace PricingKata.Tests;

public class Pricer
{
    public static string CalculatePrice(int itemCount, decimal itemPrice, decimal tax)
    {
        var priceWithoutTax = itemCount * itemPrice;

        var priceWithDiscount = priceWithoutTax - GetDiscountedAmount(priceWithoutTax);

        var taxAmount = GetTaxAmount(priceWithDiscount, tax);

        var priceWithTax = priceWithDiscount + taxAmount;

        return $"{priceWithTax.ToString("0.00", CultureInfo.InvariantCulture)} €";
    }

    private static decimal GetDiscountedAmount(decimal priceWithoutTax)
    {
        var discount = priceWithoutTax switch
        {
            > 5000 => 5m,
            > 1000 => 3m,
            _ => 0m
        };


        return priceWithoutTax * discount / 100m;
    }

    private static decimal GetTaxAmount(decimal priceWithoutTax, decimal tax)
        => priceWithoutTax * tax / 100;
}