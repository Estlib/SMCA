using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMCA.Models
{
    public class PostContent
    {
        //this class is 100% clanker code
        public Guid ID { get; set; }

        /// <summary>
        /// Local file to upload.
        /// </summary>
        public string LocalPath { get; set; } = "";

        /// <summary>
        /// Image or Video.
        /// </summary>
        public MediaType Type { get; set; }

        /// <summary>
        /// Accessibility description.
        /// </summary>
        public string? AltText { get; set; }

        /// <summary>
        /// Optional thumbnail/cover image.
        /// </summary>
        public string? ThumbnailPath { get; set; }

        /// <summary>
        /// Order in which media should appear.
        /// </summary>
        public int Order { get; set; }
    }
}

