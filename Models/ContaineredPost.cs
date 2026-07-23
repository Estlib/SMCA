using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMCA.Models
{
    //use for frontend only
    class ContaineredPost
    {
        [Key]
        public Guid ID { get; set; }
        public Post PostModel { get; set; }
        public PostSettings PostSettingsData { get; set; }
        public PostError? PostErrors { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public List<DateTime>? PublishThisAt { get; set; }
        public List<Platform>? PublishOnPlatforms { get; set; }
        public bool? IsDraft { get; set; }
        public PostType ThisPostIsA { get; set; }
    }
}
