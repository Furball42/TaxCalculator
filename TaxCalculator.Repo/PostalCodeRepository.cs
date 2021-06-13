using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TaxCalculator.Core.Models.PostalCodes;

namespace TaxCalculator.Repo
{
    public class PostalCodeRepository : Repository<PostalCode>, IPostalCodeRepository
    {
        public PostalCodeRepository(TaxCalculatorDbContext context) : base(context)
        {
        }

        public async Task<PostalCode> GetByCode(string code)
        {
            try
            {
                return await _context.PostalCodes.FirstOrDefaultAsync(p => p.Description == code);
            }
            catch (Exception)
            {
                throw new Exception("Postal Code not on record.");
            }
        }

        public async Task<PostalCode> GetByIdSilently(int id)
        {
            try
            {
                return await _context.PostalCodes.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception)
            {
                throw new Exception("Postal Code not on record.");
            }
        }
    }
}