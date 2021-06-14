namespace TaxCalculator.Core.Helpers
{
    public static class CalculationsHelper
    {
        public static decimal CalculatePercentageOf(decimal value, decimal percentage)
        {
            return value / 100 * percentage;
        }
    }
}