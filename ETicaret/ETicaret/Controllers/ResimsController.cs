using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ETicaret.Models;

namespace ETicaret.Controllers
{
    public class ResimsController : Controller
    {
        private readonly ETicaretNewContext _context;

        public ResimsController(ETicaretNewContext context)
        {
            _context = context;
        }

        // GET: Resims
        public async Task<IActionResult> Index()
        {
            var eTicaretNewContext = _context.Resims.Include(r => r.Urun);
            return View(await eTicaretNewContext.ToListAsync());
        }

        // GET: Resims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resim = await _context.Resims
                .Include(r => r.Urun)
                .FirstOrDefaultAsync(m => m.ResimId == id);
            if (resim == null)
            {
                return NotFound();
            }

            return View(resim);
        }

        // GET: Resims/Create
        public IActionResult Create()
        {
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId");
            return View();
        }

        // POST: Resims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResimId,Resim1,UrunId")] Resim resim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId", resim.UrunId);
            return View(resim);
        }

        // GET: Resims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resim = await _context.Resims.FindAsync(id);
            if (resim == null)
            {
                return NotFound();
            }
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId", resim.UrunId);
            return View(resim);
        }

        // POST: Resims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResimId,Resim1,UrunId")] Resim resim)
        {
            if (id != resim.ResimId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResimExists(resim.ResimId))
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
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId", resim.UrunId);
            return View(resim);
        }

        // GET: Resims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resim = await _context.Resims
                .Include(r => r.Urun)
                .FirstOrDefaultAsync(m => m.ResimId == id);
            if (resim == null)
            {
                return NotFound();
            }

            return View(resim);
        }

        // POST: Resims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resim = await _context.Resims.FindAsync(id);
            if (resim != null)
            {
                _context.Resims.Remove(resim);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResimExists(int id)
        {
            return _context.Resims.Any(e => e.ResimId == id);
        }
    }
}
