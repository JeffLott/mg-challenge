using Mg.Challenge.Core.Builders;
using Mg.Challenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mg.Challenge.Core.Services
{
    public class FileParsingService
    {
        public FileDto Parse(string[] lines)
        {
            var fileBuilder = new FileBuilder();

            var result = fileBuilder.Build(lines);

            return result.Item1;
        }
    }
}
