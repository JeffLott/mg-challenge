using Mg.Challenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mg.Challenge.Core.Builders
{
    public class ItemBuilder : SequentialDtoBuilder<ItemDto>
    {
        public ItemBuilder() : base("L")
        {
            RegisterMapping(1, (dto, str) => dto.Sku = str);
            RegisterMapping(2, (dto, num) => dto.Qty = num);
        }
    }
}
