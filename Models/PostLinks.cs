using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMCA.Models
{
    public class PostLinks
    {
        public Guid ID { get; set; }
        public string? XLINK { get; set; }
        public string? RLINK { get; set; }
        public string? TLINK { get; set; }
        public string? ILINK { get; set; }
        public string? BLINK { get; set; }
    }
}
