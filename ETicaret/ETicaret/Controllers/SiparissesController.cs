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
    public class SiparissesController : Controller
    {
        private readonly ETicaretNewContext _context;

        public SiparissesController(ETicaretNewContext context)
        {
            _context = context;
        }

        // GET: Siparisses
        public async Task<IActionResult> Index()
        {
            var eTicaretNewContext = _context.Siparisses.Include(s => s.Adres).Include(s => s.Uye);
            return View(await eTicaretNewContext.ToListAsync());
        }

        // GET: Siparisses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sipariss = await _context.Siparisses
                .Include(s => s.Adres)
                .Include(s => s.Uye)
                .FirstOrDefaultAsync(m => m.SiparisId == id);
            if (sipariss == null)
            {
                return NotFound();
            }

            return View(sipariss);
        }

        // GET: Siparisses/Create
        public IActionResult Create()
        {
            ViewData["AdresId"] = new SelectList(_context.Adres, "AdresId", "AdresId");
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId");
            return View();
        }

        // POST: Siparisses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SiparisId,UyeId,ToplamTutar,AdresId,ÖdemeDurumu,SiparisTarihi,TeslimTarihi")] Sipariss sipariss)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sipariss);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresId"] = new SelectList(_context.Adres, "AdresId", "AdresId", sipariss.AdresId);
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId", sipariss.UyeId);
            return View(sipariss);
        }

        // GET: Siparisses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sipariss = await _context.Siparisses.FindAsync(id);
            if (sipariss == null)
            {
                return NotFound();
            }
            ViewData["AdresId"] = new SelectList(_context.Adres, "AdresId", "AdresId", sipariss.AdresId);
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId", sipariss.UyeId);
            return View(sipariss);
        }

        // POST: Siparisses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SiparisId,UyeId,ToplamTutar,AdresId,ÖdemeDurumu,SiparisTarihi,TeslimTarihi")] Sipariss sipariss)
        {
            if (id != sipariss.SiparisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sipariss);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiparissExists(sipariss.SiparisId))
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
            ViewData["AdresId"] = new SelectList(_context.Adres, "AdresId", "AdresId", sipariss.AdresId);
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId", sipariss.UyeId);
            return View(sipariss);
        }

        // GET: Siparisses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sipariss = await _context.Siparisses
                .Include(s => s.Adres)
                .Include(s => s.Uye)
                .FirstOrDefaultAsync(m => m.SiparisId == id);
            if (sipariss == null)
            {
                return NotFound();
            }

            return View(sipariss);
        }

        // POST: Siparisses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sipariss = await _context.Siparisses.FindAsync(id);
            if (sipariss != null)
            {
                _context.Siparisses.Remove(sipariss);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiparissExists(int id)
        {
            return _context.Siparisses.Any(e => e.SiparisId == id);
        }
    }
}
