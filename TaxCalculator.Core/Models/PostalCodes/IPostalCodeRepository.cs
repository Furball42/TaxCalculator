using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Core.Models.PostalCodes
{
    public interface IPostalCodeRepository : IRepository<PostalCode>
    {
        PostalCode GetByCode(string code);
    }
}
