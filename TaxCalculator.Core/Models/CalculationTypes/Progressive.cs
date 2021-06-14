using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaxCalculator.Core.Dtos;
using TaxCalculator.Core.Helpers;

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
                        if (level.Max == -1)
                            level.Max = annualIncome; //flag for infinite above upper level

                        var previousMax = (level.Min - 1) < 0 ? 0 : level.Min - 1;

                        var taxableThisRate = Math.Min(level.Max - previousMax, annualIncome - previousMax);
                        var taxOnThisLevel = CalculationsHelper.CalculatePercentageOf(taxableThisRate, level.Rate);
                        totalTax += taxOnThisLevel;
                    }
                }

                return totalTax;
            }
            else
                throw new ArgumentException("No negative income value allowed.");
        }

        public List<ProgressiveTaxByLevelDto> CalculateTaxPerLevel(decimal annualIncome)
        {
            var taxLevels = DeserializeExtendedData();
            var returnList = new List<ProgressiveTaxByLevelDto>();

            if (taxLevels.Count == 0)
                throw new Exception("Reference data empty");

            foreach (var level in taxLevels)
            {
                if (annualIncome > level.Min)
                {
                    if (level.Max == -1)
                        level.Max = annualIncome; //flag for infinite above upper level

                    var previousMax = (level.Min - 1) < 0 ? 0 : level.Min - 1;

                    var taxableThisRate = Math.Min(level.Max - previousMax, annualIncome - previousMax);
                    var taxOnThisLevel = CalculationsHelper.CalculatePercentageOf(taxableThisRate, level.Rate);

                    returnList.Add(new ProgressiveTaxByLevelDto()
                    {
                        LevelTax = taxOnThisLevel,
                        Max = level.Max,
                        Min = level.Min,
                        Rate = level.Rate,
                    });
                }
            }

            return returnList;
        }

        private List<ProgressiveTypeValues> DeserializeExtendedData()
        {
            if (string.IsNullOrEmpty(ExtendedData))
                return new List<ProgressiveTypeValues>();

            var xxx = JsonConvert.DeserializeObject<List<ProgressiveTypeValues>>(ExtendedData);
            return xxx;
        }
    }
}