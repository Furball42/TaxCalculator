using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxCalculator.Core.Models.CalculationTypes;

namespace TaxCalculator.Repo
{
    public class ProgressiveRepository : Repository<Progressive>, IProgressiveRepository
    {
        public ProgressiveRepository(TaxCalculatorDbContext context) : base(context)        
        {

        }

        public Progressive GetFirstAvailable()
        {
            return _context.Progressives.FirstOrDefault();
        }
    }
}
