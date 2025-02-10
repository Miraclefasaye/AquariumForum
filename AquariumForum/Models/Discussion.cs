using System;
using System.Collections.Generic;
using System.Linq;

namespace AquariumForum.Models
{
    public class Discussion
    {
        public int DiscussionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ImageFilename { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        // Computed property to get the count of comments
        public int CommentCount => Comments?.Count ?? 0;
    }
}
