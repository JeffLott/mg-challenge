using Mg.Challenge.Core.Builders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mg.Challenge.Tests.Builders
{
    [TestFixture]
    public class FileBuilderTests
    {
        [Test]
        public void BuildsCorrectly()
        {
            var testData = new[] { "\"F\",\"08/04/2018\",\" By Batch #\"" };

            var sut = new FileBuilder();

            var result = sut.Build(testData).Item1;

            Assert.AreEqual(result.Type, " By Batch #");
            Assert.AreEqual(result.Date, new DateTime(2018, 08, 04));
        }

        [Test]
        public void BuildsChildren()
        {
            var testData = new[] {
                "\"F\",\"08/04/2018\",\" By Batch #\"",
                "\"O\",\"08/04/2018\",\"ONF002793300\",\"080427bd1\"",
                "\"T\",\"3\",\"3\",\"0\",\"2\",\"0\"",
                "\"B\",\"Brett Nagy\",\"5825 221st Place S.E.\",\"98027\"",
                "\"E\",\"1\",\"2\",\"9\""
            };

            var sut = new FileBuilder();

            var result = sut.Build(testData).Item1;

            Assert.NotNull(result.Ender);
            Assert.AreEqual(1, result.Orders.Count);
        }
    }
}
