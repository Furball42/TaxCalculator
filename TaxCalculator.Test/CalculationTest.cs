using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Core.Models.CalculationTypes;

namespace TaxCalculator.Test
{
    [TestFixture]
    public class CalculationTest
    {
        #region Flat Rate

        [Test]
        public void FlatRate_InputValue_SmallerThanZero_ArgumentException_Success()
        {
            var flatRate = new FlatRate
            {
                Rate = 17
            };

            Assert.Throws<ArgumentException>(() => flatRate.CalculateResult(-200));
        }

        [Test]
        public void FlatRate_ShouldReturn_CalculatedValue ()
        {
            var flatRate = new FlatRate
            {
                Rate = 17
            };
            var result = flatRate.CalculateResult(22000);

            Assert.IsNotNull(result);
            Assert.AreEqual(3740, result);
        }

        #endregion

        #region Flat Value

        [Test]
        public void FlatValue_InputValue_SmallerThanZero_ArgumentException_Success()
        {
            var flatValue = new FlatValue
            {
                Percentage = 5,
                ValueThreshold = 200000,
                Value = 10000,
            };

            Assert.Throws<ArgumentException>(() => flatValue.CalculateResult(-200));
        }

        [Test]
        public void FlatValue_ShouldReturn_FixedValue()
        {
            var flatValue = new FlatValue
            {
                Percentage = 5,
                ValueThreshold = 200000,
                Value = 10000,
            };
            var result = flatValue.CalculateResult(220000);

            Assert.IsNotNull(result);
            Assert.AreEqual(10000, result);
        }        

        [Test]
        public void FlatValue_ShouldReturn_DynamicValue()
        {
            var flatValue = new FlatValue
            {
                Percentage = 5,
                ValueThreshold = 200000,
                Value = 10000,
            };
            var result = flatValue.CalculateResult(150000);

            Assert.IsNotNull(result);
            Assert.AreEqual(7500, result);
        }

        #endregion

        #region Progressive

        [Test]
        public void Progressive_InputValue_SmallerThanZero_ArgumentException_Success()
        {
            var progressive = new Progressive
            {
                ExtendedData = BuildProgressiveData(),
            };

            Assert.Throws<ArgumentException>(() => progressive.CalculateResult(-200));
        }

        [Test]
        public void Progressive_ReferenceDataCount_SmallerThanZero_Exception_Success()
        {
            var progressive = new Progressive
            {
                ExtendedData = string.Empty,
            };

            Assert.Throws<Exception>(() => progressive.CalculateResult(150000));
        }

        [Test]
        public void Progressive_ShouldReturn_DynamicValue()
        {
            var progressive = new Progressive
            {
                ExtendedData = BuildProgressiveData(),
            };

            var result = progressive.CalculateResult(150000);

            //TODO: maybe change all test to look for a list
            Assert.IsNotNull(result);
            Assert.AreEqual(35719.320, result);
        }

        private string BuildProgressiveData()
        {
            var unserializedList = new List<ProgressiveTypeValues>()
            {
                new ProgressiveTypeValues() { SortOrder = 1, Rate = 10, Min = 0, Max = 8350 },
                new ProgressiveTypeValues() { SortOrder = 2, Rate = 15, Min = 8351, Max = 33950 },
                new ProgressiveTypeValues() { SortOrder = 3, Rate = 25, Min = 33951, Max = 82250 },
                new ProgressiveTypeValues() { SortOrder = 4, Rate = 28, Min = 82251, Max = 171550 },
                new ProgressiveTypeValues() { SortOrder = 5, Rate = 33, Min = 171551, Max = 372950 },
                new ProgressiveTypeValues() { SortOrder = 6, Rate = 35, Min = 372951, Max = -1 }
            };

            return JsonConvert.SerializeObject(unserializedList);
        }

        #endregion
    }
}
