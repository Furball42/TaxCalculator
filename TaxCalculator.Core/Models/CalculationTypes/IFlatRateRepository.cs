using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Core.Models.CalculationTypes
{
    public interface  IFlatRateRepository : IRepository<FlatRate>
    {
        FlatRate GetFirstAvailable();
    }
}
