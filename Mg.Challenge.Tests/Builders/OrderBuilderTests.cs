using Mg.Challenge.Core.Builders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mg.Challenge.Tests.Builders
{
    [TestFixture]
    public class OrderBuilderTests
    {
        [Test]
        public void BuildsCorrectly()
        {
            var testData = new[] { "\"O\",\"08/04/2018\",\"ONF002793300\",\"080427bd1\"" };

            var sut = new OrderBuilder();

            var result = sut.Build(testData);

            Assert.AreEqual(result.Date, new DateTime(2018, 08, 04));
            Assert.AreEqual(result.Code, "ONF002793300");
            Assert.AreEqual(result.Number, "080427bd1");
        }

        [Test]
        public void BuildsChildren()
        {
            var testData = new[] {
                "\"O\",\"08/04/2018\",\"ONF002793300\",\"080427bd1\"",
                "\"T\",\"3\",\"3\",\"0\",\"2\",\"0\"",
                 "\"B\",\"Brett Nagy\",\"5825 221st Place S.E.\",\"98027\""
                };

            var sut = new OrderBuilder();

            var result = sut.Build(testData);

            Assert.NotNull(result.Timings);
            Assert.NotNull(result.Buyer);
        }
    }
}
