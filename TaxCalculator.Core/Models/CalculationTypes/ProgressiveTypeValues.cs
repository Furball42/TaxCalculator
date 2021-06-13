using System.ComponentModel.DataAnnotations.Schema;

namespace TaxCalculator.Core.Models.CalculationTypes
{
    public class ProgressiveTypeValues
    {
        [Column(TypeName = "decimal(4,2)")]
        public decimal Rate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Min { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Max { get; set; }

        public int SortOrder { get; set; }
    }
}