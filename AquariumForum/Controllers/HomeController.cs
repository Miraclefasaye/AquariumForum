using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AquariumForum.Data;
using AquariumForum.Models;
using AquariumForum.ViewModels; // Add this line
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AquariumForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Home/Index - Displays the discussions in descending order
        public async Task<IActionResult> Index()
        {
            var discussions = await _context.Discussions
                .OrderByDescending(d => d.CreateDate)
                .Select(d => new DiscussionViewModel
                {
                    DiscussionId = d.DiscussionId,
                    Title = d.Title,
                    ImageFilename = d.ImageFilename,
                    CreateDate = d.CreateDate,
                    CommentCount = d.Comments.Count
                })
                .ToListAsync();

            return View(discussions);
        }
    }
}
