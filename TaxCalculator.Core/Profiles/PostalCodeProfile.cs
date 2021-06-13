using AutoMapper;
using TaxCalculator.Core.Dtos;
using TaxCalculator.Core.Models.PostalCodes;

namespace TaxCalculator.Core.Profiles
{
    public class PostalCodeProfile : Profile
    {
        public PostalCodeProfile()
        {
            CreateMap<PostalCode, PostalCodeListOutputDto>()
                .ForMember(dest =>
                    dest.CalculationTypeDescription,
                    opt => opt.MapFrom(src => src.CalculationType.ToString()));
        }
    }
}