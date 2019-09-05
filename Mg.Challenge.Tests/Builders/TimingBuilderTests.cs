using Mg.Challenge.Core.Builders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mg.Challenge.Tests.Builders
{
    [TestFixture]
    public class TimingBuilderTests
    {
        [Test]
        public void BuildsCorrectly()
        {
            var testData = new[] { "\"T\",\"3\",\"3\",\"0\",\"2\",\"0\"" };

            var sut = new TimingBuilder();

            var result = sut.Build(testData).Item1;

            Assert.AreEqual(result.Start, 3);
            Assert.AreEqual(result.Stop, 3);
            Assert.AreEqual(result.Gap, 0);
            Assert.AreEqual(result.Offset, 2);
            Assert.AreEqual(result.Pause, 0);
        }
    }
}
