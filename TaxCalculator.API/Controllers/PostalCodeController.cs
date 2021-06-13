using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Core.Dtos;
using TaxCalculator.Core.Enums;
using TaxCalculator.Core.Models;
using TaxCalculator.Core.Models.PostalCodes;

namespace TaxCalculator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostalCodeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostalCodeController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Route("GetPostalCodes")]
        public async Task<List<PostalCodeListOutputDto>> GetPostalCodes()
        {
            var list = await _unitOfWork.PostalCodes.GetAll();
            var mapped = _mapper.Map<List<PostalCodeListOutputDto>>(list.ToList());
            return mapped;
        }

        [Route("DeletePostalCode/{id}")]
        public async Task DeletePostalCode(int id)
        {
            var postalCode = await _unitOfWork.PostalCodes.Get(id);

            if(postalCode == null)
                throw new Exception("Postal Code not on record.");

            _unitOfWork.PostalCodes.Delete(postalCode);
            _unitOfWork.Complete();
        }

        [Route("PutPostalCode")]
        public async Task PutPostalCode(PostalCode postalCode)
        {
            //this step is arguable - is here for error checking
            var postalCodeCurrent = await _unitOfWork.PostalCodes.GetByIdSilently(postalCode.Id);            
            if (postalCodeCurrent == null)
                throw new Exception("Postal Code not on record.");

            _unitOfWork.PostalCodes.Update(postalCode);
            _unitOfWork.Complete();
        }

        [Route("GetCalculationTypes")]
        public List<CalculationTypeListDto> GetCalculationTypes()
        {
            var list = new List<CalculationTypeListDto>();
            var types = Enum.GetValues(typeof(CalculationTypeEnum))
                            .Cast<CalculationTypeEnum>()
                            .ToList();

            foreach (var type in types)
            {
                list.Add(new CalculationTypeListDto() { Description = type.ToString(), Id = (int)type });
            }

            return list;
        }

        //specific for datatables
        [Route("GetPostalCodesForDatables")]
        public async Task<DataTablesResponseDto> GetPostalCodesForDatables()
        {
            var list = await _unitOfWork.PostalCodes.GetAll();
            var mapped = _mapper.Map<List<PostalCodeListOutputDto>>(list.ToList());
            return new DataTablesResponseDto()
            {
                Data = mapped.ToArray(),
                RecordsTotal = mapped.Count,
            };
        }

        [Route("PostPostalCode")]
        public async Task<bool> PostPostalCode(PostalCode postalCode)
        {
            //this bit is to set the reference id to the first availble type in db matching the Calc type
            //this will probably be removed if we move on to selecting a specific saved type from the db
            //but this was not in the scope, but code was added for future expansion
            //for now, it is "hard coded" to the first
            postalCode.ReferenceId = await DetermineCalculationTypeAndReturnFirstIntanceId(postalCode.CalculationType);

            //furthermore, check can be done here (and probably in the db rules) for duplicate Postal Codes

            await _unitOfWork.PostalCodes.Add(postalCode);
            _unitOfWork.Complete();

            return true;
        }

        private async Task<int> DetermineCalculationTypeAndReturnFirstIntanceId(CalculationTypeEnum type)
        {
            switch (type)
            {
                case Core.Enums.CalculationTypeEnum.FlatRate:

                    var flatRateType = await _unitOfWork.FlatRates.GetFirstAvailable();
                    return flatRateType.Id;

                case Core.Enums.CalculationTypeEnum.FlatValue:

                    var flatValueType = await _unitOfWork.FlatValues.GetFirstAvailable();
                    return flatValueType.Id;

                case Core.Enums.CalculationTypeEnum.Progressive:

                    var progressionType = await _unitOfWork.Progressives.GetFirstAvailable();
                    return progressionType.Id;

                default:
                    throw new Exception("Calculation Type not found.");
            }
        }
    }
}