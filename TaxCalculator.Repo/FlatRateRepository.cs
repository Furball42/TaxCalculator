using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxCalculator.Core.Models.CalculationTypes;

namespace TaxCalculator.Repo
{
    public class FlatRateRepository : Repository<FlatRate>, IFlatRateRepository
    {
        public FlatRateRepository(TaxCalculatorDbContext context) : base(context)
        {
            
        }

        public FlatRate GetFirstAvailable()
        {
            return _context.FlatRates.FirstOrDefault();
        }
    }
}
