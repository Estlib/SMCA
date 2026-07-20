using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMCA.Models
{
    public class PostSettings
    {
        public Guid SID { get; set; }
        public string RelatedPost { get; set; }
        public bool WillNotify { get; set; }
        public bool IsWindowOrBalloon { get; set; }
        public bool? ShowLinks { get; set; }
        public bool? LogErrors { get; set; } = true;

    }
}
