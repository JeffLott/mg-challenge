using Mg.Challenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mg.Challenge.Core.Builders
{
    public class TimingBuilder : SequentialDtoBuilder<TimingsDto>
    {
        public TimingBuilder() : base("T")
        {
            RegisterMapping(1, (dto, num) => dto.Start = num);
            RegisterMapping(2, (dto, num) => dto.Stop = num);
            RegisterMapping(3, (dto, num) => dto.Gap = num);
            RegisterMapping(4, (dto, num) => dto.Offset = num);
            RegisterMapping(5, (dto, num) => dto.Pause = num);
        }
    }
}
