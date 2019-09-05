using System;
using System.Collections.Generic;
using System.Text;

namespace Mg.Challenge.Core.Models
{
    public class OrderDto
    {
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public string Number { get; set; }
        public BuyerDto Buyer { get; set; }
        public List<ItemDto> Items { get; set; }
        public TimingsDto Timings { get; set; }
    }
}
