using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMCA.Models
{
    public class PostError
    {
        public Guid EID { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public string RelatedPost { get; set; }
        public List<Platform>? FailedPlatforms { get; set; }
        public Platform ThisFailedPlatform { get; set; }
    }
}
