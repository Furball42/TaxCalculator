using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Core.Dtos
{
    public class ProgressiveTaxByLevelDto
    {
        public decimal Rate { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public decimal LevelTax { get; set; }
    }
}
