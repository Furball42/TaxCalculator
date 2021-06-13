using System.Threading.Tasks;

namespace TaxCalculator.Core.Models.PostalCodes
{
    public interface IPostalCodeRepository : IRepository<PostalCode>
    {
        Task<PostalCode> GetByCode(string code);
    }
}