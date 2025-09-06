using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var latestExponats = await _context.Exponats
                .Include(e => e.Author)
                .Include(e => e.Collection)
                .Include(e => e.Location)
                .OrderByDescending(e => e.CreationYear)
                .Take(6)
                .ToListAsync();

            return View(latestExponats);
        }
    }
}
