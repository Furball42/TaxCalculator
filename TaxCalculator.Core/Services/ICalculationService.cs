using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Core.Dtos;

namespace TaxCalculator.Core.Services
{
    public interface ICalculationService : IDomainService
    {
        CalculationResultDto ReturnDtoAndSave(decimal annualIncome, string postalCode);
    }
}
