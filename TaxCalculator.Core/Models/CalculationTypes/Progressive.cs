using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaxCalculator.Core.Models.CalculationTypes
{
    public class Progressive : CalculationTypeBase
    {
        [Required]
        public string ExtendedData { get; set; }

        public override  decimal CalculateResult()
        {
            throw new NotImplementedException();
        }
    }
}
