using Mg.Challenge.Core.Builders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mg.Challenge.Tests.Builders
{
    [TestFixture]
    class BuyerBuilderTests
    {
        [Test]
        public void BuildsCorrectly()
        {
            var testData = new[] { "\"B\",\"Brett Nagy\",\"5825 221st Place S.E.\",\"98027\"" };

            var sut = new BuyerBuilder();

            var result = sut.Build(testData).Item1;

            Assert.AreEqual(result.Name, "Brett Nagy");
            Assert.AreEqual(result.Street, "5825 221st Place S.E.");
            Assert.AreEqual(result.Zip, "98027");
        }
    }
}
