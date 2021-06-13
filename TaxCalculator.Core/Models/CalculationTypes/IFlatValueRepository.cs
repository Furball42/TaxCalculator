using System.Threading.Tasks;

namespace TaxCalculator.Core.Models.CalculationTypes
{
    public interface IFlatValueRepository : IRepository<FlatValue>
    {
        Task<FlatValue> GetFirstAvailable();
    }
}