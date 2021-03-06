using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxCalculator.Core.Models.CalculationResults
{
    public class CalculationResult
    {
        public int Id { get; set; }
        public DateTime DateTimeCreated { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AnnualIncome { get; set; }

        public string PostalCode { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CalculatedTax { get; set; }
    }
}