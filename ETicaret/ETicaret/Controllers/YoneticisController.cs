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
    public class YoneticisController : Controller
    {
        private readonly ETicaretNewContext _context;

        public YoneticisController(ETicaretNewContext context)
        {
            _context = context;
        }

        // GET: Yoneticis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Yoneticis.ToListAsync());
        }

        // GET: Yoneticis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yonetici = await _context.Yoneticis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yonetici == null)
            {
                return NotFound();
            }

            return View(yonetici);
        }

        // GET: Yoneticis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Yoneticis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KullaniciAdi,Email,Password,Durum")] Yonetici yonetici)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yonetici);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yonetici);
        }

        // GET: Yoneticis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yonetici = await _context.Yoneticis.FindAsync(id);
            if (yonetici == null)
            {
                return NotFound();
            }
            return View(yonetici);
        }

        // POST: Yoneticis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KullaniciAdi,Email,Password,Durum")] Yonetici yonetici)
        {
            if (id != yonetici.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yonetici);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YoneticiExists(yonetici.Id))
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
            return View(yonetici);
        }

        // GET: Yoneticis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yonetici = await _context.Yoneticis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yonetici == null)
            {
                return NotFound();
            }

            return View(yonetici);
        }

        // POST: Yoneticis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yonetici = await _context.Yoneticis.FindAsync(id);
            if (yonetici != null)
            {
                _context.Yoneticis.Remove(yonetici);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YoneticiExists(int id)
        {
            return _context.Yoneticis.Any(e => e.Id == id);
        }
    }
}
