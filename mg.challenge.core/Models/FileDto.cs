using System;
using System.Collections.Generic;
using System.Text;

namespace Mg.Challenge.Core.Models
{
    public class FileDto
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public IEnumerable<OrderDto> Orders { get; set; }
        public TimingsDto Timings { get; set; }
    }
}
