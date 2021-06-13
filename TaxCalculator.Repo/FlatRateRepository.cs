using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaxCalculator.Core.Models.CalculationTypes;

namespace TaxCalculator.Repo
{
    public class FlatRateRepository : Repository<FlatRate>, IFlatRateRepository
    {
        public FlatRateRepository(TaxCalculatorDbContext context) : base(context)
        {
        }

        public async Task<FlatRate> GetFirstAvailable()
        {
            return await _context.FlatRates.FirstOrDefaultAsync();
        }
    }
}