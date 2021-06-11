using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaxCalculator.Core.Models.CalculationTypes
{
    public class FlatRate : CalculationTypeBase
    {
        [Required]
        [Column(TypeName = "decimal(4,2)")]
        public decimal Rate { get; set; }

        public override decimal CalculateResult()
        {
            throw new NotImplementedException();
        }
    }
}
