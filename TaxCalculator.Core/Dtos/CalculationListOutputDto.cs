using System;

namespace TaxCalculator.Core.Dtos
{
    public class CalculationListOutputDto
    {
        public int Id { get; set; }
        public DateTime DateTimeCreated { get; set; }

        public decimal AnnualIncome { get; set; }

        public string PostalCode { get; set; }

        public decimal CalculatedTax { get; set; }
    }
}