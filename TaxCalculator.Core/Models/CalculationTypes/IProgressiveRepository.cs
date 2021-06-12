using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Core.Models.CalculationTypes
{
    public interface IProgressiveRepository : IRepository<Progressive>
    {
        Task<Progressive> GetFirstAvailable();
    }
}
