using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AquariumForum.Data;
using AquariumForum.Models;
using System.IO;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace AquariumForum.Controllers
{
    public class DiscussionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DiscussionsController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Discussions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Discussions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Discussion discussion, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                // Handle image upload and resizing
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Generate a unique filename (using GUID or DateTime)
                    var fileExtension = Path.GetExtension(imageFile.FileName);
                    var fileName = $"{Guid.NewGuid()}{fileExtension}";
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", fileName);

                    // Resize the image to a thumbnail (100px width for example)
                    using (var imageStream = imageFile.OpenReadStream())
                    {
                        using (var img = Image.Load(imageStream))
                        {
                            img.Mutate(x => x.Resize(100, 0)); // Resize width to 100px, height auto
                            await img.SaveAsync(filePath, new JpegEncoder()); // Save as JPEG
                        }
                    }

                    // Store the filename in the database
                    discussion.ImageFilename = fileName;
                }

                // Save the discussion to the database
                _context.Add(discussion);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));  // Redirect to the list of discussions (or wherever you want)
            }

            return View(discussion);
        }
    }
}
