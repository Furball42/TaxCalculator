using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxCalculator.Core.Models.CalculationTypes;

namespace TaxCalculator.Repo
{
    public class FlatValueRepository : Repository<FlatValue>, IFlatValueRepository
    {
        public FlatValueRepository(TaxCalculatorDbContext context) : base(context)
        {

        }

        public FlatValue GetFirstAvailable()
        {
            return _context.FlatValues.FirstOrDefault();
        }
    }
}
