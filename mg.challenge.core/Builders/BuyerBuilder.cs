using Mg.Challenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mg.Challenge.Core.Builders
{
    public class BuyerBuilder : SequentialDtoBuilder<BuyerDto>
    {
        public BuyerBuilder() : base("B")
        {
            RegisterMapping(1, (dto, str) => dto.Name = str);
            RegisterMapping(2, (dto, str) => dto.Street = str);
            RegisterMapping(3, (dto, str) => dto.Zip = str);
        }
    }
}
