using Mg.Challenge.Core.Builders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mg.Challenge.Tests.Builders
{
    [TestFixture]
    public class EnderBuilderTests
    {
        [Test]
        public void BuildsCorrectly()
        {
            var testData = new[] { "\"E\",\"1\",\"2\",\"9\"" };

            var sut = new EnderBuilder();

            var result = sut.Build(testData).Item1;

            Assert.AreEqual(result.Process, 1);
            Assert.AreEqual(result.Paid, 2);
            Assert.AreEqual(result.Created, 9);
        }
    }
}
