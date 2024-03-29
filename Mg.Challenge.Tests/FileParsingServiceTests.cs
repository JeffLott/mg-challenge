using FluentAssertions;
using Mg.Challenge.Core.Models;
using Mg.Challenge.Core.Services;
using Newtonsoft.Json;
using NUnit.Framework;
using System;

namespace Tests
{
    public class FileParsingServiceTests
    {
        [Test]
        public void ParsingTest()
        {
            var data = TestInput.Trim().Split(Environment.NewLine);

            var sut = new FileParsingService();

            var response = sut.Parse(data);

            var expected = JsonConvert.DeserializeObject<FileDto>(TestOutput);

            Assert.NotNull(response);

            response.Should().BeEquivalentTo(expected);
        }

        #region TestData

        private const string TestInput = @"
            ""F"",""08/04/2018"","" By Batch #""
            ""O"",""08/04/2018"",""ONF002793300"",""080427bd1""
            ""B"",""Brett Nagy"",""5825 221st Place S.E."",""98027""
            ""L"",""602527788265"",""02""
            ""L"",""602517642850"",""01""
            ""T"",""3"",""3"",""0"",""2"",""0""
            ""E"",""1"",""2"",""9""";

        private const string TestOutput = @"
        {
            ""date"":""08/04/2018"",
            ""type"":"" By Batch #"",
            ""orders"":[
                {
                    ""date"":""08/04/2018"",
                    ""code"":""ONF002793300"",
                    ""number"":""080427bd1"",
                    ""buyer"":{
                        ""name"":""Brett Nagy"",
                        ""street"":""5825 221st Place S.E."",
                        ""zip"":""98027""
                    },
                    ""items"":[
                        {
                            ""sku"":""602527788265"",
                            ""qty"":2
                        },
                        {
                            ""sku"":""602517642850"",
                            ""qty"":1
                        }
                    ],
                    ""timings"":{
                        ""start"":3,
                        ""stop"":3,
                        ""gap"":0,
                        ""offset"":2,
                        ""pause"":0
                    }
                }
            ],
            ""ender"":{
                ""process"":1,
                ""paid"":2,
                ""created"":9
            }
        }";

        #endregion
    }
}