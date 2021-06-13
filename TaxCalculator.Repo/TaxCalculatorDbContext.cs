using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using TaxCalculator.Core.Models.CalculationResults;
using TaxCalculator.Core.Models.CalculationTypes;
using TaxCalculator.Core.Models.PostalCodes;

namespace TaxCalculator.Repo
{
    public class TaxCalculatorDbContext : DbContext
    {
        public DbSet<FlatRate> FlatRates { get; set; }
        public DbSet<FlatValue> FlatValues { get; set; }
        public DbSet<Progressive> Progressives { get; set; }
        public DbSet<CalculationResult> CalculationResult { get; set; }
        public DbSet<PostalCode> PostalCodes { get; set; }

        public TaxCalculatorDbContext(DbContextOptions<TaxCalculatorDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FlatRate>()
                .HasData(new FlatRate
                {
                    Id = 1,
                    Description = "Flat Rate",
                    Rate = 17.5m,
                });

            modelBuilder.Entity<FlatValue>()
                .HasData(new FlatValue
                {
                    Id = 1,
                    Description = "Flat Rate",
                    Value = 10000,
                    ValueThreshold = 200000,
                    Percentage = 5.0m,
                });

            //dummy seeding values for first dummy Progressive
            var progressiveSeedList = new List<ProgressiveTypeValues>
            {
                new ProgressiveTypeValues() { SortOrder = 1, Rate = 10, Min = 0, Max = 8350 },
                new ProgressiveTypeValues() { SortOrder = 2, Rate = 15, Min = 8351, Max = 33950 },
                new ProgressiveTypeValues() { SortOrder = 3, Rate = 25, Min = 33951, Max = 82250 },
                new ProgressiveTypeValues() { SortOrder = 4, Rate = 28, Min = 82251, Max = 171550 },
                new ProgressiveTypeValues() { SortOrder = 5, Rate = 33, Min = 171551, Max = 372950 },
                new ProgressiveTypeValues() { SortOrder = 6, Rate = 35, Min = 372951, Max = -1 }
            };

            var serializedSeedList = JsonConvert.SerializeObject(progressiveSeedList);

            modelBuilder.Entity<Progressive>()
                .HasData(new Progressive
                {
                    Id = 1,
                    Description = "Progressive",
                    ExtendedData = serializedSeedList,
                });

            modelBuilder.Entity<PostalCode>()
                .HasData(new PostalCode
                {
                    Id = 1,
                    Description = "7441",
                    CalculationType = Core.Enums.CalculationTypeEnum.Progressive,
                    ReferenceId = 1,
                }, new PostalCode
                {
                    Id = 2,
                    Description = "A100",
                    CalculationType = Core.Enums.CalculationTypeEnum.FlatValue,
                    ReferenceId = 1,
                }, new PostalCode
                {
                    Id = 3,
                    Description = "7000",
                    CalculationType = Core.Enums.CalculationTypeEnum.FlatRate,
                    ReferenceId = 1,
                }, new PostalCode
                {
                    Id = 4,
                    Description = "1000",
                    CalculationType = Core.Enums.CalculationTypeEnum.Progressive,
                    ReferenceId = 1,
                });
        }
    }
}