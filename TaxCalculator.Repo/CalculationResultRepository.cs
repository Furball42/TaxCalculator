using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Core.Models.CalculationResults;

namespace TaxCalculator.Repo
{
    public class CalculationResultRepository : Repository<CalculationResult>, ICalculationResultRepository
    {
        public CalculationResultRepository(TaxCalculatorDbContext context) : base(context)
        {
            
        }
    }
}
