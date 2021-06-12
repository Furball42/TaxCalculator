using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Core.Models.CalculationTypes;

namespace TaxCalculator.Repo
{
    public class ProgressiveRepository : Repository<Progressive>, IProgressiveRepository
    {
        public ProgressiveRepository(TaxCalculatorDbContext context) : base(context)        
        {

        }

        public async Task<Progressive> GetFirstAvailable()
        {
            return await _context.Progressives.FirstOrDefaultAsync();
        }
    }
}
