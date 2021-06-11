using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaxCalculator.Core.Models.CalculationTypes
{
    public abstract class CalculationTypeBase : ICalculationType
    {
        [Key]
        public int Id { get; set; }
        
        [Required, MaxLength(100)]
        public string Description { get; set; }

        public abstract decimal CalculateResult();
    }
}
