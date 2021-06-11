using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TaxCalculator.API.Controllers;
using TaxCalculator.Core.Models;
using TaxCalculator.Core.Models.PostalCodes;
using TaxCalculator.Repo;

namespace TaxCalculator.Test
{
    public class PostalCodeTest
    {
        public SortedList<string, decimal> TestUserEntries = new SortedList<string, decimal>();
        public List<PostalCode> TestAgainstCodes = new List<PostalCode>();

        public PostalCodeTest()
        {

        }

        [SetUp]
        public void Setup()
        {
            TestAgainstCodes = GetPostalCodesForTesting();
            TestUserEntries = UserEntries();
        }

        [Test]
        public void PostalCode_GetByCode_ShouldReturnCorrectCodeByCode()
        {
           
            //var result = _unitOfWork.PostalCodes.GetByCode(TestUserEntries.Keys[1]);

            //Assert.IsNotNull(result);
            //Assert.AreEqual(TestAgainstCodes[1].Description, result.Description);
        }

        private SortedList<string, decimal> UserEntries()
        {
            return new SortedList<string, decimal>()
            {
                { "0000", 120000 }, //result: fail
                { "7441", 88000 },
            };
        }

        private List<PostalCode> GetPostalCodesForTesting()
        {
            return new List<PostalCode>
            {
                new PostalCode()
                {
                    Id = 1,
                    CalculationType = Core.Enums.CalculationTypeEnum.Progressive,
                    Description = "7441",
                    ReferenceId = 1,
                },

                new PostalCode()
                {
                    Id = 2,
                    CalculationType = Core.Enums.CalculationTypeEnum.FlatValue,
                    Description = "A100",
                    ReferenceId = 1,
                },

                new PostalCode()
                {
                    Id = 3,
                    CalculationType = Core.Enums.CalculationTypeEnum.FlatRate,
                    Description = "7000",
                    ReferenceId = 1,
                },

                new PostalCode()
                {
                    Id = 4,
                    CalculationType = Core.Enums.CalculationTypeEnum.Progressive,
                    Description = "1000",
                    ReferenceId = 1,
                }
            };
        }
    }
}
