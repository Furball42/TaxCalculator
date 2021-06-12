using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Core.Dtos
{
    public class CalculationResultDto
    {
        public decimal OriginalIncome { get; set; }
        public decimal IncomeAfterTax { get; set; }
        public decimal TotalTaxes { get; set; }
        public decimal TotalMonthlyTaxes { get; set; }
        public decimal TotalTaxPercentage { get; set; }
    }
}
