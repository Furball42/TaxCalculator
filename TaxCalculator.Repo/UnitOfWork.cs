using System;
using TaxCalculator.Core.Models;
using TaxCalculator.Core.Models.CalculationResults;
using TaxCalculator.Core.Models.CalculationTypes;
using TaxCalculator.Core.Models.PostalCodes;

namespace TaxCalculator.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaxCalculatorDbContext _context;

        public ICalculationResultRepository CalculationResults { get; }
        public IPostalCodeRepository PostalCodes { get; }
        public IFlatRateRepository FlatRates { get; }
        public IFlatValueRepository FlatValues { get; }
        public IProgressiveRepository Progressives { get; }

        public UnitOfWork(TaxCalculatorDbContext context,
            ICalculationResultRepository calcResultRepo,
            IPostalCodeRepository postalCodeRepo,
            IFlatRateRepository flatRateRepo,
            IFlatValueRepository flatValueRepo,
            IProgressiveRepository progressiveRepo
            )
        {
            _context = context;
            CalculationResults = calcResultRepo;
            PostalCodes = postalCodeRepo;
            FlatRates = flatRateRepo;
            FlatValues = flatValueRepo;
            Progressives = progressiveRepo;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}