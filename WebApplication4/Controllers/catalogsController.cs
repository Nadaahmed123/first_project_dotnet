
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class catalogsController : Controller
    {
        private readonly libraryContext _context;

        public catalogsController(libraryContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            return _context.catalogs != null ?
                        View(await _context.catalogs.ToListAsync()) :
                        Problem("Entity set 'BlogContext.catalogs'  is null.");
        }

        // GET: catalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.catalogs == null)
            {
                return NotFound();
            }

            var catalog = await _context.catalogs
                .FirstOrDefaultAsync(m => m.cat_id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        // GET: catalogs/Create
        public IActionResult Create()
        {
            return View();
        }
            [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("cat_id,cat_name,cat_desc")] catalog catalog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(catalog);
        }

        // GET: catalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.catalogs == null)
            {
                return NotFound();
            }

            var catalog = await _context.catalogs.FindAsync(id);
            if (catalog == null)
            {
                return NotFound();
            }
            return View(catalog);
        }
      [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("cat_id,cat_name,cat_desc")] catalog catalog)
        {
            if (id != catalog.cat_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!catalogExists(catalog.cat_id))
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
            return View(catalog);
        }

        // GET: catalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.catalogs == null)
            {
                return NotFound();
            }

            var catalog = await _context.catalogs
                .FirstOrDefaultAsync(m => m.cat_id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        // POST: catalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.catalogs == null)
            {
                return Problem("Entity set 'BlogContext.catalogs'  is null.");
            }
            var catalog = await _context.catalogs.FindAsync(id);
            if (catalog != null)
            {
                _context.catalogs.Remove(catalog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool catalogExists(int id)
        {
            return (_context.catalogs?.Any(e => e.cat_id == id)).GetValueOrDefault();
        }
    }
}

