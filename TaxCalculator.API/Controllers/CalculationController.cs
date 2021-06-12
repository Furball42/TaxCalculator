using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.API.Dtos;
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
        public CalculationResultDto DoTaxCalculation(decimal annualIncome, string code)
        {
            if (code == null)
                throw new Exception("Invalid postal code.");

            //get tax type
            var postalCode = _unitOfWork.PostalCodes.GetByCode(code);

            if(postalCode == null)
                throw new Exception("Postal Code not on record.");

            //TODO: Comment over decision here and probs domain service?
            //OR CONVERT TO ID
            //TODO: Save to db after successful calc
            var totalTax = 0m;
            switch (postalCode.CalculationType) {

                case Core.Enums.CalculationTypeEnum.FlatRate:

                    var flatRateType = _unitOfWork.FlatRates.GetFirstAvailable();
                    totalTax = flatRateType.CalculateResult(annualIncome);
                    break;

                case Core.Enums.CalculationTypeEnum.FlatValue:

                    var flatValueType = _unitOfWork.FlatRates.GetFirstAvailable();
                    totalTax = flatValueType.CalculateResult(annualIncome);
                    break;

                case Core.Enums.CalculationTypeEnum.Progressive:

                    var progressionType = _unitOfWork.FlatRates.GetFirstAvailable();
                    totalTax = progressionType.CalculateResult(annualIncome);
                    break;

                //TODO: Handle this result
                default:
                    throw new Exception("Postal Code contains no tax details.");
            }

            //save to DB TODO: probs move this
            _unitOfWork.CalculationResults.Add(new Core.Models.CalculationResults.CalculationResult()
            {
                AnnualIncome = annualIncome,
                CalculatedTax = totalTax,
                DateTimeCreated = DateTime.Now,
                PostalCode = postalCode.Description
            });
            _unitOfWork.Complete();

            return BuildResultDto(annualIncome, totalTax);
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

        private CalculationResultDto BuildResultDto(decimal originalIncome, decimal taxTotal)
        {
            return new CalculationResultDto()
            {
                OriginalIncome = originalIncome,
                TotalTaxes = taxTotal,
                IncomeAfterTax = originalIncome - taxTotal,
                TotalMonthlyTaxes = taxTotal / 12,
                TotalTaxPercentage = taxTotal / originalIncome * 100,
            };
        }
    }
}
