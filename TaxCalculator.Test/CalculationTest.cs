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
        [Test]
        public void FlatRate_ShouldReturn_CalculatedValue ()
        {
            var flatRate = new FlatRate
            {
                Rate = 17
            };
            var result = flatRate.CalculateResult(22000);

            Assert.IsNotNull(result);
            Assert.AreEqual(17000, result);
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
        public void FlatValue_ShouldReturn_Dynamic()
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
    }
}
