using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Core.Models.PostalCodes;

namespace TaxCalculator.Repo
{
    public class PostalCodeRepository : Repository<PostalCode>, IPostalCodeRepository
    {
        public PostalCodeRepository(TaxCalculatorDbContext context) : base(context)
        {

        }

        public PostalCode GetByCode(string code)
        {
            return  (PostalCode)_context.PostalCodes.Where(p => p.Description == code);
        }
    }
}
