using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Core.Dtos;
using TaxCalculator.Core.Models;
using TaxCalculator.Core.Models.CalculationTypes;
using TaxCalculator.Core.Models.PostalCodes;

namespace TaxCalculator.Core.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CalculationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CalculationResultDto ReturnDtoAndSave(decimal annualIncome, string postalCode)
        {
            var code = GetPostalCode(postalCode);
            var totalTax = 0m;

            if (code == null)
                throw new Exception("Postal Code not on record.");

            switch (code.CalculationType)
            {

                case Core.Enums.CalculationTypeEnum.FlatRate:

                    var flatRateType = _unitOfWork.FlatRates.GetFirstAvailable();
                    totalTax = flatRateType.CalculateResult(annualIncome);
                    break;

                case Core.Enums.CalculationTypeEnum.FlatValue:

                    var flatValueType = _unitOfWork.FlatValues.GetFirstAvailable();
                    totalTax = flatValueType.CalculateResult(annualIncome);
                    break;

                case Core.Enums.CalculationTypeEnum.Progressive:

                    var progressionType = _unitOfWork.Progressives.GetFirstAvailable();
                    totalTax = progressionType.CalculateResult(annualIncome);
                    break;

                //TODO: Handle this result
                default:
                    throw new Exception("Postal Code contains no tax details.");
            }

            var result = BuildResult(annualIncome, totalTax);

            try
            {
                SaveToDb(annualIncome, totalTax, code);
                return result;
            }
            catch (Exception)
            {
                throw new Exception("Error writing to database.");
            }
            
        }

        private PostalCode GetPostalCode(string postalCode)
        {
            return _unitOfWork.PostalCodes.GetByCode(postalCode);
        }

        private CalculationResultDto BuildResult(decimal originalIncome, decimal taxTotal)
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

        private void SaveToDb(decimal annualIncome, decimal totalTax, PostalCode postalCode)
        {
            _unitOfWork.CalculationResults.Add(new Core.Models.CalculationResults.CalculationResult()
            {
                AnnualIncome = annualIncome,
                CalculatedTax = totalTax,
                DateTimeCreated = DateTime.Now,
                PostalCode = postalCode.Description
            });
            _unitOfWork.Complete();
        }
    }
}
