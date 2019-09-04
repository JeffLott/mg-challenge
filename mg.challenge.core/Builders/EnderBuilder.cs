using Mg.Challenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mg.Challenge.Core.Builders
{
    public class EnderBuilder : SequentialDtoBuilder<EnderDto>
    {
        public EnderBuilder() : base("E")
        {
            RegisterMapping(1, (dto, num) => dto.Process = num);
            RegisterMapping(2, (dto, num) => dto.Paid = num);
            RegisterMapping(3, (dto, num) => dto.Created = num);
        }
    }
}
