using Mg.Challenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mg.Challenge.Core.Builders
{
    public class OrderBuilder : SequentialDtoBuilder<OrderDto>
    {
        public OrderBuilder() : base("O")
        {
            RegisterMapping(1, (dto, date) => dto.Date = date);
            RegisterMapping(2, (dto, str) => dto.Code = str);
            RegisterMapping(3, (dto, str) => dto.Number = str);
            RegisterChildDto(new TimingBuilder(), (dto, timing) => dto.Timings = timing);
            RegisterChildDto(new BuyerBuilder(), (dto, buyer) => dto.Buyer = buyer);
            RegisterChildDto(new ItemBuilder(), (dto, item) =>
            {
                if (dto.Items == null)
                    dto.Items = new List<ItemDto>();

                dto.Items.Add(item);
            });
        }
    }
}
