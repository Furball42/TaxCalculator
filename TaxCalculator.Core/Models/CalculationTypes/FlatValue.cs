using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaxCalculator.Core.Helpers;

namespace TaxCalculator.Core.Models.CalculationTypes
{
    public class FlatValue : CalculationTypeBase
    {
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValueThreshold { get; set; }

        [Column(TypeName = "decimal(4,2)")]
        public decimal Percentage { get; set; }

        public override decimal CalculateResult(decimal annualIncome)
        {
            if (annualIncome > 0)
            {
                if (annualIncome > ValueThreshold)
                    return Value;
                else
                    return CalculationsHelper.CalculatePercentageOf(annualIncome, Percentage);
            }
            else
                throw new ArgumentException("No negative income value allowed.");
        }
    }
}