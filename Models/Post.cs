using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMCA.Models
{
    public class Post
    {
        public Guid ID { get; set; }
        public string PostModelID { get; set; }
        public string PostSettingID { get; set; }
        public string PostErrorsIDs { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public List<DateTime>? PublishThisAt { get; set; }
        public List<Platform>? PublishOnPlatforms { get; set; }
        public bool IsDraft { get; set; }

    }
}
