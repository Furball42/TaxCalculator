using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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

        public override decimal CalculateResult()
        {
            throw new NotImplementedException();
        }
    }
}
