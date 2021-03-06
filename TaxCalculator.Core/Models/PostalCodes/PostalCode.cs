using System.ComponentModel.DataAnnotations;
using TaxCalculator.Core.Enums;

namespace TaxCalculator.Core.Models.PostalCodes
{
    public class PostalCode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public CalculationTypeEnum CalculationType { get; set; }

        //the reasoning behind this: opens up extensibility when wanting to add more than one
        //of a certain type: ie. FLatRate10, FlatRate17
        public int ReferenceId { get; set; }
    }
}