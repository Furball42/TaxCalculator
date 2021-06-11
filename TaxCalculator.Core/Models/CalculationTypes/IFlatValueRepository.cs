using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Core.Models.CalculationTypes
{
    public interface IFlatValueRepository : IRepository<FlatValue>
    {
        FlatValue GetFirstAvailable();
    }
}
