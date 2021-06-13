using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaxCalculator.Core.Models.CalculationTypes;

namespace TaxCalculator.Repo
{
    public class FlatValueRepository : Repository<FlatValue>, IFlatValueRepository
    {
        public FlatValueRepository(TaxCalculatorDbContext context) : base(context)
        {
        }

        public async Task<FlatValue> GetFirstAvailable()
        {
            return await _context.FlatValues.FirstOrDefaultAsync();
        }
    }
}