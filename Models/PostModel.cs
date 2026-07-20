using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMCA.Models
{
    public class PostModel
    {
        //this class is 100% clanket code
        public Guid ID { get; set; }
        /// <summary>
        /// Main body of the post.
        /// Used by: Twitter (text), Bluesky (text), Reddit (selftext), Instagram (caption), Threads (text)
        /// </summary>
        public string? MainText { get; set; }

        /// <summary>
        /// Title shown above the post.
        /// Used by: Reddit (title)
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Collection of attached media (images/videos).
        /// Used by: Twitter, Bluesky, Reddit, Instagram, Threads
        /// </summary>
        public List<PostContent> Media { get; set; } = new();

        /// <summary>
        /// Accessibility descriptions for attached media.
        /// Used by: Twitter (alt_text), Bluesky (alt), Reddit (gallery captions), Instagram (alt_text), Threads (alt_text)
        /// </summary>
        public List<string> MediaAltText { get; set; } = new();

        /// <summary>
        /// Thumbnail or cover image for attached video.
        /// Used by: Twitter, Bluesky, Reddit, Instagram (Reels), Threads
        /// </summary>
        public string? CoverImage { get; set; }

        /// <summary>
        /// External website URL associated with the post.
        /// Used by: Twitter, Bluesky, Reddit, Instagram, Threads
        /// </summary>
        public string? LinkUrl { get; set; }

        /// <summary>
        /// Title displayed in an external link preview.
        /// Used by: Bluesky
        /// </summary>
        public string? LinkTitle { get; set; }

        /// <summary>
        /// Description displayed in an external link preview.
        /// Used by: Bluesky
        /// </summary>
        public string? LinkDescription { get; set; }

        /// <summary>
        /// Thumbnail displayed in an external link preview.
        /// Used by: Bluesky
        /// </summary>
        public string? LinkThumbnail { get; set; }

        /// <summary>
        /// Existing post that this post replies to.
        /// Used by: Twitter, Bluesky, Reddit, Threads
        /// </summary>
        public string? ReplyTarget { get; set; }

        /// <summary>
        /// Root post of the discussion thread.
        /// Used by: Bluesky
        /// </summary>
        public string? ThreadRoot { get; set; }

        /// <summary>
        /// Existing post to repost/share.
        /// Used by: Twitter (Retweet), Bluesky (Repost), Reddit (Crosspost)
        /// </summary>
        public string? RepostTarget { get; set; }

        /// <summary>
        /// Existing post quoted together with new commentary.
        /// Used by: Twitter, Bluesky
        /// </summary>
        public string? QuoteTarget { get; set; }

        /// <summary>
        /// Poll answer choices.
        /// Used by: Twitter, Reddit
        /// </summary>
        public List<string> PollOptions { get; set; } = new();

        /// <summary>
        /// Poll duration.
        /// Used by: Twitter, Reddit
        /// </summary>
        public TimeSpan? PollDuration { get; set; }

        /// <summary>
        /// Hashtags associated with the post.
        /// Used by: Twitter, Bluesky, Reddit, Instagram, Threads
        /// </summary>
        public List<string> Hashtags { get; set; } = new();

        /// <summary>
        /// User mentions.
        /// Used by: Twitter, Bluesky, Reddit, Instagram, Threads
        /// </summary>
        public List<string> Mentions { get; set; } = new();

        /// <summary>
        /// Language code.
        /// Used by: Bluesky
        /// </summary>
        public string? Language { get; set; }

        /// <summary>
        /// Destination community or subreddit.
        /// Used by: Twitter (Community), Reddit (Subreddit)
        /// </summary>
        public string? Community { get; set; }

        /// <summary>
        /// Whether a community post should also appear in followers' feeds.
        /// Used by: Twitter
        /// </summary>
        public bool? ShareToFollowers { get; set; }

        /// <summary>
        /// Reddit flair identifier.
        /// Used by: Reddit
        /// </summary>
        public string? Flair { get; set; }

        /// <summary>
        /// Human-readable flair text.
        /// Used by: Reddit
        /// </summary>
        public string? FlairText { get; set; }

        /// <summary>
        /// Indicates that the content contains mature or sensitive material.
        /// Used by: Bluesky, Reddit
        /// </summary>
        public bool? NSFW { get; set; }

        /// <summary>
        /// Indicates that the post contains spoilers.
        /// Used by: Reddit
        /// </summary>
        public bool? Spoiler { get; set; }

        /// <summary>
        /// Indicates that the content is original work.
        /// Used by: Reddit
        /// </summary>
        public bool? OriginalContent { get; set; }

        /// <summary>
        /// Indicates that AI was used to create the content.
        /// Used by: Twitter, Bluesky, Instagram, Threads
        /// </summary>
        public bool? AIDisclosure { get; set; }

        /// <summary>
        /// Indicates sponsored or branded content.
        /// Used by: Twitter, Instagram, Threads
        /// </summary>
        public bool? PaidPartnership { get; set; }

        /// <summary>
        /// Users tagged inside attached media.
        /// Used by: Twitter, Instagram
        /// </summary>
        public List<string> UserTags { get; set; } = new();

        /// <summary>
        /// Accounts publishing the post together.
        /// Used by: Instagram
        /// </summary>
        public List<string> Collaborators { get; set; } = new();

        /// <summary>
        /// Attached geographic location.
        /// Used by: Twitter, Instagram, Threads
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// Audience restrictions or reply permissions.
        /// Used by: Twitter, Reddit, Instagram, Threads
        /// </summary>
        public string? Audience { get; set; }

        ///// <summary>
        ///// When the application should publish the post.
        ///// Application property (not sent directly to platform APIs).
        ///// Used by: All platforms
        ///// </summary>
        //public DateTime? ScheduledPublish { get; set; }

        ///// <summary>
        ///// Indicates the post is still a draft.
        ///// Application property.
        ///// Used by: All platforms
        ///// </summary>
        //public bool IsDraft { get; set; }

        ///// <summary>
        ///// Platform-specific overrides for any property (e.g. different text per platform).
        ///// Application property.
        ///// Used by: All platforms
        ///// </summary>
        //public Dictionary<SocialPlatform, PlatformOverride> PlatformOverrides { get; set; } = new();
    }
}
