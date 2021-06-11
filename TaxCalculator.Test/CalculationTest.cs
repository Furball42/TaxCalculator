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
    }
}
