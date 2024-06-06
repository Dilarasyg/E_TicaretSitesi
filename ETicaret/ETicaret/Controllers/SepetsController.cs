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
    public class SepetsController : Controller
    {
        private readonly ETicaretNewContext _context;

        public SepetsController(ETicaretNewContext context)
        {
            _context = context;
        }

        // GET: Sepets
        public async Task<IActionResult> Index()
        {
            var eTicaretNewContext = _context.Sepets.Include(s => s.Urun).Include(s => s.Uye);
            return View(await eTicaretNewContext.ToListAsync());
        }

        // GET: Sepets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepet = await _context.Sepets
                .Include(s => s.Urun)
                .Include(s => s.Uye)
                .FirstOrDefaultAsync(m => m.SepetId == id);
            if (sepet == null)
            {
                return NotFound();
            }

            return View(sepet);
        }

        // GET: Sepets/Create
        public IActionResult Create()
        {
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId");
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId");
            return View();
        }

        // POST: Sepets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SepetId,UyeId,UrunId,Adet,EklenmeTarihi")] Sepet sepet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sepet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId", sepet.UrunId);
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId", sepet.UyeId);
            return View(sepet);
        }

        // GET: Sepets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepet = await _context.Sepets.FindAsync(id);
            if (sepet == null)
            {
                return NotFound();
            }
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId", sepet.UrunId);
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId", sepet.UyeId);
            return View(sepet);
        }

        // POST: Sepets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SepetId,UyeId,UrunId,Adet,EklenmeTarihi")] Sepet sepet)
        {
            if (id != sepet.SepetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sepet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SepetExists(sepet.SepetId))
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
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId", sepet.UrunId);
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId", sepet.UyeId);
            return View(sepet);
        }

        // GET: Sepets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepet = await _context.Sepets
                .Include(s => s.Urun)
                .Include(s => s.Uye)
                .FirstOrDefaultAsync(m => m.SepetId == id);
            if (sepet == null)
            {
                return NotFound();
            }

            return View(sepet);
        }

        // POST: Sepets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sepet = await _context.Sepets.FindAsync(id);
            if (sepet != null)
            {
                _context.Sepets.Remove(sepet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SepetExists(int id)
        {
            return _context.Sepets.Any(e => e.SepetId == id);
        }
    }
}
