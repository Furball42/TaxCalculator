using AutoMapper;
using TaxCalculator.Core.Dtos;
using TaxCalculator.Core.Models.CalculationResults;

namespace TaxCalculator.Core.Profiles
{
    public class CalculationProfile : Profile
    {
        public CalculationProfile()
        {
            CreateMap<CalculationResult, CalculationListOutputDto>();
        }
    }
}