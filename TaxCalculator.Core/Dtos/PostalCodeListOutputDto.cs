using TaxCalculator.Core.Enums;

namespace TaxCalculator.Core.Dtos
{
    public class PostalCodeListOutputDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public CalculationTypeEnum CalculationType { get; set; }
        public int ReferenceId { get; set; }
    }
}