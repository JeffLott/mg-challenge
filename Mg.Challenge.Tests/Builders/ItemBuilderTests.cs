using Mg.Challenge.Core.Builders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mg.Challenge.Tests.Builders
{
    public class ItemBuilderTests
    {
        [Test]
        public void BuildsCorrectly()
        {
            var testData = new[] { "\"L\",\"602527788265\",\"02\"" };

            var sut = new ItemBuilder();

            var result = sut.Build(testData).Item1;

            Assert.AreEqual(result.Sku, "602527788265");
            Assert.AreEqual(result.Qty, 2);
        }
    }
}
