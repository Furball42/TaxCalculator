using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using TaxCalculator.API.Controllers;
using TaxCalculator.Core.Dtos;
using TaxCalculator.Core.Models;
using TaxCalculator.Core.Models.CalculationResults;
using TaxCalculator.Core.Models.CalculationTypes;
using TaxCalculator.Core.Models.PostalCodes;
using TaxCalculator.Core.Profiles;
using TaxCalculator.Repo;

namespace TaxCalculator.Test
{
    //these are more web api tests than proper unit tests but still valuable

    [TestFixture]
    public class PostalCodeAPITest
    {
        public SortedList<string, decimal> TestUserEntries = new SortedList<string, decimal>();
        public List<PostalCode> TestAgainstCodes = new List<PostalCode>();

        public PostalCodeAPITest()
        {

        }

        [SetUp]
        public void Setup()
        {
            TestAgainstCodes = GetPostalCodesForTesting();
            TestUserEntries = UserEntries();
        }

        [Test]
        public void PostalCode_Insert_ShouldReturnTrue()
        {
            var postalCodeList = GetPostalCodesForTesting();
            var postalCodeRepositoryMock = new Mock<IPostalCodeRepository>();
            postalCodeRepositoryMock.Setup(m => m.GetByIdSilently(2)).ReturnsAsync(postalCodeList[2]).Verifiable();

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(m => m.PostalCodes).Returns(postalCodeRepositoryMock.Object);

            var pcProfile = new PostalCodeProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(pcProfile));
            IMapper mapper = new Mapper(configuration);

            var apiController = new PostalCodeController(unitOfWork.Object, mapper);

            var result = apiController.PutPostalCode(new PostalCode() { 
                CalculationType = Core.Enums.CalculationTypeEnum.FlatRate,
                Description = "XXXX",
                Id = 2,
                ReferenceId = 1,
            });

            //Assert.IsTrue(result.);
        }

        [Test]
        public void PostalCode_GetCalculationTypes_ShouldNotReturnEmpty()
        {
            var postalCodeRepositoryMock = new Mock<IPostalCodeRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(m => m.PostalCodes).Returns(postalCodeRepositoryMock.Object);

            var pcProfile = new PostalCodeProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(pcProfile));
            IMapper mapper = new Mapper(configuration);

            var apiController = new PostalCodeController(unitOfWork.Object, mapper);

            var result = apiController.GetCalculationTypes();

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void PostalCode_GetPostalCodes_ShouldNotReturnEmpty()
        {

            var postalCodeList = GetPostalCodesForTesting();

            var postalCodeRepositoryMock = new Mock<IPostalCodeRepository>();
            postalCodeRepositoryMock.Setup(m => m.GetAll()).ReturnsAsync(postalCodeList).Verifiable();

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(m => m.PostalCodes).Returns(postalCodeRepositoryMock.Object);

            var pcProfile = new PostalCodeProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(pcProfile));
            IMapper mapper = new Mapper(configuration);

            var apiController = new PostalCodeController(unitOfWork.Object, mapper);

            var result = apiController.GetAllPostalCodes ().Result;


            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
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
