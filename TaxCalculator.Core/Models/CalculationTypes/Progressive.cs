using Newtonsoft.Json;
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

        public override decimal CalculateResult(decimal annualIncome)
        {
            if (annualIncome > 0)
            {
                var taxLevels = DeserializeExtendedData();

                if (taxLevels.Count == 0)
                    throw new Exception("Reference data empty");

                var totalTax = 0m;
                
                foreach (var level in taxLevels)
                {
                    if (annualIncome > level.Min)
                    {
                        var taxableThisRate = Math.Min(level.Max - level.Min, annualIncome - level.Min);
                        var taxOnThisLevel = taxableThisRate / 100 * level.Rate;
                        totalTax += taxOnThisLevel;
                    }
                }

                return totalTax;
            }
            else
                throw new ArgumentException("No negative income value allowed.");
        }

        private List<ProgressiveTypeValues> DeserializeExtendedData()
        {
            if (string.IsNullOrEmpty(ExtendedData))
                return new List<ProgressiveTypeValues>();

            var xxx =  JsonConvert.DeserializeObject<List<ProgressiveTypeValues>>(ExtendedData);
            return xxx;
        }
    }
}
