using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Core.Models;
using TaxCalculator.Core.Models.CalculationResults;
using TaxCalculator.Core.Models.CalculationTypes;
using TaxCalculator.Core.Models.PostalCodes;
using TaxCalculator.Core.Services;

namespace TaxCalculator.Repo
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IFlatRateRepository, FlatRateRepository>();
            services.AddTransient<IFlatValueRepository, FlatValueRepository>();
            services.AddTransient<ICalculationResultRepository, CalculationResultRepository>();
            services.AddTransient<IProgressiveRepository, ProgressiveRepository>();
            services.AddTransient<IPostalCodeRepository, PostalCodeRepository>();
            services.AddTransient<ICalculationService, CalculationService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<TaxCalculatorDbContext>(opt => opt
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=taxCalcDb;persist security info=True; Integrated Security = SSPI;"));

            return services;
        }
    }
}