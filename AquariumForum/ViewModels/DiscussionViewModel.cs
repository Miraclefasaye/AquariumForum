using System;

namespace AquariumForum.ViewModels
{
    public class DiscussionViewModel
    {
        public int DiscussionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageFilename { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public int CommentCount { get; set; }
    }
}
