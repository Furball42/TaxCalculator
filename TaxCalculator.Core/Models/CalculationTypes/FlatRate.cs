using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxCalculator.Core.Models.CalculationTypes
{
    public class FlatRate : CalculationTypeBase
    {
        [Required]
        [Column(TypeName = "decimal(4,2)")]
        public decimal Rate { get; set; }

        public override decimal CalculateResult(decimal annualIncome)
        {
            if (annualIncome > 0)
                return annualIncome / 100 * Rate;
            else
                throw new ArgumentException("No negative income value allowed.");
        }
    }
}