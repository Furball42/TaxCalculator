using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public PostalCodeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("GetPostalCodes")]
        [HttpGet]
        public async Task<List<PostalCode>> GetPostalCodes()
        {
            var list = await _unitOfWork.PostalCodes.GetAll();
            return list.ToList();
        }

        [Route("PostPostalCode")]
        [HttpPost]
        public async Task PostPostalCode(PostalCode postalCode)
        {
            //this bit is to set the reference id to the first availble type in db matching the Calc type
            //this will probably be removed if we move on to selecting a specific saved type from the db
            //but this was not in the scope, but code was added for future expansion
            //for now, it is "hard coded" to the first
            postalCode.ReferenceId = await DetermineCalculationTypeAndReturnFirstIntanceId(postalCode.CalculationType);

            //furthermore, check can be done here (and probably in the db rules) for duplicate Postal Codes

            await _unitOfWork.PostalCodes.Add(postalCode);
            _unitOfWork.Complete();
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