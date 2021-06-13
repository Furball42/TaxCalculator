using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Core.Enums
{
    public enum CalculationTypeEnum
    {
        [Display(Name = "Flat Value")]
        FlatValue = 0,

        [Display(Name = "Flat Rate")]
        FlatRate = 1,

        [Display(Name = "Progressive")]
        Progressive = 2,
    }
}