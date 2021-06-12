using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Core.Models;
using TaxCalculator.Core.Models.CalculationTypes;

namespace TaxCalculator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;        

        public CalculationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("DoTaxCalculation/{annualIncome}/{code}")]
        [HttpGet]
        public async Task<decimal> DoTaxCalculation(decimal annualIncome, string code)
        {
            if (code == null)
                throw new Exception("Invalid postal code.");

            //get tax type
            var postalCode = _unitOfWork.PostalCodes.GetByCode(code);

            if(postalCode == null)
                throw new Exception("Postal Code not on record.");

            //TODO: Comment over decision here
            //OR CONVERT TO ID
            switch (postalCode.CalculationType) {

                case Core.Enums.CalculationTypeEnum.FlatRate:

                    var flatRateType = _unitOfWork.FlatRates.GetFirstAvailable();
                    return flatRateType.CalculateResult(annualIncome);

                case Core.Enums.CalculationTypeEnum.FlatValue:

                    var flatValueType = _unitOfWork.FlatRates.GetFirstAvailable();
                    return flatValueType.CalculateResult(annualIncome);

                case Core.Enums.CalculationTypeEnum.Progressive:

                    var progressionType = _unitOfWork.FlatRates.GetFirstAvailable();
                    return progressionType.CalculateResult(annualIncome);

                //TODO: Handle this result
                default:
                    throw new Exception("Postal Code contains no tax details.");
            }
        }

        [Route("GetFlatRates")]
        [HttpGet]        
        public async Task<IEnumerable<FlatRate>> GetFlatRates()
        {
            return await _unitOfWork.FlatRates.GetAll();
        }

        [Route("GetFlatRate/{id}")]
        [HttpGet]
        public async Task<FlatRate> GetFlatRateById(int id)
        {
            return await _unitOfWork.FlatRates.Get(id);
        }
    }
}
