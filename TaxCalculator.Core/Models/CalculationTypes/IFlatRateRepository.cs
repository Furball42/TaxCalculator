using System.Threading.Tasks;

namespace TaxCalculator.Core.Models.CalculationTypes
{
    public interface IFlatRateRepository : IRepository<FlatRate>
    {
        Task<FlatRate> GetFirstAvailable();
    }
}