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

            var result = sut.Build(testData);

            Assert.AreEqual(result.Type, " By Batch #");
            Assert.AreEqual(result.Date, new DateTime(2018, 08, 04));
        }
    }
}
