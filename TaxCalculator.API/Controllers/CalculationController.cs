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

        [Route("DoTaxCalculation/{annualIncome:decimal}/{postalCode:string}")]
        [HttpGet]
        public async Task<decimal> DoTaxCalculation(decimal annualIncome, string code)
        {
            //get tax type
            var postalCode = _unitOfWork.PostalCodes.GetByCode(code);

            //TODO: Comment over decision here
            //OR CONVERT TO ID
            switch (postalCode.CalculationType) {
                case Core.Enums.CalculationTypeEnum.FlatRate:

                    var flatRateType = _unitOfWork.FlatRates.GetFirstAvailable();

                    break;

                case Core.Enums.CalculationTypeEnum.FlatValue:

                    var flatValueType = _unitOfWork.FlatRates.GetFirstAvailable();

                    break;

                case Core.Enums.CalculationTypeEnum.Progressive:

                    var progressionType = _unitOfWork.FlatRates.GetFirstAvailable();

                    break;

                //TODO: Handle this result
                default:

                    break;

            }

            return 0;
        }

        [Route("getflatrates")]
        [HttpGet]        
        public async Task<IEnumerable<FlatRate>> GetFlatRates()
        {
            return await _unitOfWork.FlatRates.GetAll();
        }

        [Route("getflatrate/{id:int}")]
        [HttpGet]
        public async Task<FlatRate> GetFlatRateById(int id)
        {
            return await _unitOfWork.FlatRates.Get(id);
        }
    }
}
