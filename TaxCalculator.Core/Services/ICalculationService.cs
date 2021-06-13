using System.Threading.Tasks;
using TaxCalculator.Core.Dtos;

namespace TaxCalculator.Core.Services
{
    public interface ICalculationService : IDomainService
    {
        Task<CalculationResultDto> ReturnDtoAndSave(decimal annualIncome, string postalCode);
    }
}