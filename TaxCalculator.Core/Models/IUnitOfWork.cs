using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Core.Models.CalculationResults;
using TaxCalculator.Core.Models.CalculationTypes;
using TaxCalculator.Core.Models.PostalCodes;

namespace TaxCalculator.Core.Models
{
    public interface IUnitOfWork : IDisposable
    {
        ICalculationResultRepository CalculationResults { get; }
        IPostalCodeRepository PostalCodes { get; }
        IFlatRateRepository FlatRates { get; }  
        IFlatValueRepository FlatValues { get; }
        IProgressiveRepository Progressives { get; }

        int Complete();
    }
}
