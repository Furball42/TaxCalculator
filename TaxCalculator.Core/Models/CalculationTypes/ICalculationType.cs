namespace TaxCalculator.Core.Models.CalculationTypes
{
    public interface ICalculationType
    {
        decimal CalculateResult(decimal annualIncome);
    }
}