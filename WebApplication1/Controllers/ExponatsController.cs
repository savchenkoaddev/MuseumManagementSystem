using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ExponatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExponatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchAuthor, string searchCollection, string searchMaterial, int? year)
        {
            var exponats = _context.Exponats
                .Include(e => e.Author)
                .Include(e => e.Collection)
                .Include(e => e.Location)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchAuthor))
            {
                exponats = exponats.Where(e => e.Author.FirstName.Contains(searchAuthor) || e.Author.LastName.Contains(searchAuthor));
            }
            if (!string.IsNullOrEmpty(searchCollection))
            {
                exponats = exponats.Where(e => e.Collection.Name.Contains(searchCollection));
            }
            if (!string.IsNullOrEmpty(searchMaterial))
            {
                exponats = exponats.Where(e => e.Material.Contains(searchMaterial));
            }
            if (year.HasValue)
            {
                exponats = exponats.Where(e => e.CreationYear == year.Value);
            }

            return View(await exponats.ToListAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exponat = await _context.Exponats
                .Include(e => e.Author)
                .Include(e => e.Collection)
                .Include(e => e.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exponat == null)
            {
                return NotFound();
            }

            return View(exponat);
        }

        public IActionResult Create()
        {
            var authors = _context.Authors
                .Select(a => new
                {
                    a.Id,
                    FullName = a.FirstName + " " + a.LastName
                })
                .ToList();

            ViewData["AuthorId"] = new SelectList(authors, "Id", "FullName");
            ViewData["CollectionId"] = new SelectList(_context.Collections, "Id", "Name");
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Address");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Material,Technique,CreationYear,Condition,AuthorId,CollectionId,LocationId")] Exponat exponat)
        {
            ModelState.Remove("Author");
            ModelState.Remove("Collection");
            ModelState.Remove("Location");

            if (ModelState.IsValid)
            {
                exponat.Id = Guid.NewGuid();
                _context.Add(exponat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var authors = (await _context.Authors.ToListAsync())
                .Select(a => new
                {
                    a.Id,
                    FullName = a.FirstName + " " + a.LastName
                })
                .ToList();

            ViewData["AuthorId"] = new SelectList(authors, "Id", "FullName", exponat.AuthorId);
            ViewData["CollectionId"] = new SelectList(_context.Collections, "Id", "Name", exponat.CollectionId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Address", exponat.LocationId);
            return View(exponat);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exponat = await _context.Exponats.FindAsync(id);
            if (exponat == null)
            {
                return NotFound();
            }

            var authors = (await _context.Authors.ToListAsync())
                .Select(a => new
                {
                    a.Id,
                    FullName = a.FirstName + " " + a.LastName
                })
                .ToList();

            ViewData["AuthorId"] = new SelectList(authors, "Id", "FullName", exponat.AuthorId);
            ViewData["CollectionId"] = new SelectList(_context.Collections, "Id", "Name", exponat.CollectionId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Address", exponat.LocationId);
            return View(exponat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Material,Technique,CreationYear,Condition,AuthorId,CollectionId,LocationId")] Exponat exponat)
        {
            if (id != exponat.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Author");
            ModelState.Remove("Collection");
            ModelState.Remove("Location");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exponat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExponatExists(exponat.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var authors = (await _context.Authors.ToListAsync())
                .Select(a => new
                {
                    a.Id,
                    FullName = a.FirstName + " " + a.LastName
                })
                .ToList();

            ViewData["AuthorId"] = new SelectList(authors, "Id", "FullName", exponat.AuthorId);
            ViewData["CollectionId"] = new SelectList(_context.Collections, "Id", "Name", exponat.CollectionId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Address", exponat.LocationId);
            return View(exponat);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exponat = await _context.Exponats
                .Include(e => e.Author)
                .Include(e => e.Collection)
                .Include(e => e.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exponat == null)
            {
                return NotFound();
            }

            return View(exponat);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var exponat = await _context.Exponats.FindAsync(id);
            if (exponat != null)
            {
                _context.Exponats.Remove(exponat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExponatExists(Guid id)
        {
            return _context.Exponats.Any(e => e.Id == id);
        }
    }
}
