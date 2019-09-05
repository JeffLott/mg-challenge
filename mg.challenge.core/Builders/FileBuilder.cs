using Mg.Challenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mg.Challenge.Core.Builders
{
    public class FileBuilder : SequentialDtoBuilder<FileDto>
    {
        public FileBuilder() : base("F")
        {
            RegisterMapping(1, (dto, dt) => dto.Date = dt);
            RegisterMapping(2, (dto, str) => dto.Type = str);
            RegisterChildDto(new EnderBuilder(), (dto, ender) => dto.Ender = ender);
        }
    }
}
