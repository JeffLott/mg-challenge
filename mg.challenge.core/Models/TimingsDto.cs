using System;
using System.Collections.Generic;
using System.Text;

namespace Mg.Challenge.Core.Models
{
    public class TimingsDto
    {
        public int Start { get; set; }
        public int Stop { get; set; }
        public int Gap { get; set; }
        public int Offset { get; set; }
        public int Pause { get; set; }
    }
}
