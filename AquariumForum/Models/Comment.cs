using System;

namespace AquariumForum.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int DiscussionId { get; set; }
        public DateTime CreateDate { get; set; }

        public Discussion Discussion { get; set; }  // Navigation property
    }
};
