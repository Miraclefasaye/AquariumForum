using Microsoft.AspNetCore.Mvc;
using AquariumForum.Data;
using AquariumForum.Models;
using System.Threading.Tasks;

namespace AquariumForum.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comments/Create
        public IActionResult Create(int discussionId)
        {
            var comment = new Comment
            {
                DiscussionId = discussionId // Pre-fill the DiscussionId
            };

            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CreateDate = DateTime.UtcNow;
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetDiscussion", "Home", new { id = comment.DiscussionId });
            }

            return View(comment);
        }

    }
}
