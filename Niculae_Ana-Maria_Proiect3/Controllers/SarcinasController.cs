using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Niculae_Ana_Maria_Proiect3.Data;
using Niculae_Ana_Maria_Proiect3.Models;

namespace Niculae_Ana_Maria_Proiect3.Controllers
{
    public class SarcinasController : Controller
    {
        private readonly LibraryContext _context;

        public SarcinasController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Sarcinas
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Sarcini.Include(s => s.ProiectAsociat);
            return View(await libraryContext.ToListAsync());
        }

        // GET: Sarcinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sarcini == null)
            {
                return NotFound();
            }

            var sarcina = await _context.Sarcini
                .Include(s => s.ProiectAsociat)
                .FirstOrDefaultAsync(m => m.SarcinaId == id);
            if (sarcina == null)
            {
                return NotFound();
            }

            return View(sarcina);
        }

        // GET: Sarcinas/Create
        public IActionResult Create()
        {
            ViewData["ProiectId"] = new SelectList(_context.Proiecte, "ProiectId", "Nume");
            return View();
        }

        // POST: Sarcinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SarcinaId,Descriere,DataIncepere,DataFinalizare,Status,ProiectId")] Sarcina sarcina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sarcina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProiectId"] = new SelectList(_context.Proiecte, "ProiectId", "Nume", sarcina.ProiectId);
            return View(sarcina);
        }

        // GET: Sarcinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sarcini == null)
            {
                return NotFound();
            }

            var sarcina = await _context.Sarcini.FindAsync(id);
            if (sarcina == null)
            {
                return NotFound();
            }
            ViewData["ProiectId"] = new SelectList(_context.Proiecte, "ProiectId", "Nume", sarcina.ProiectId);
            return View(sarcina);
        }

        // POST: Sarcinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SarcinaId,Descriere,DataIncepere,DataFinalizare,Status,ProiectId")] Sarcina sarcina)
        {
            if (id != sarcina.SarcinaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sarcina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SarcinaExists(sarcina.SarcinaId))
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
            ViewData["ProiectId"] = new SelectList(_context.Proiecte, "ProiectId", "Nume", sarcina.ProiectId);
            return View(sarcina);
        }

        // GET: Sarcinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sarcini == null)
            {
                return NotFound();
            }

            var sarcina = await _context.Sarcini
                .Include(s => s.ProiectAsociat)
                .FirstOrDefaultAsync(m => m.SarcinaId == id);
            if (sarcina == null)
            {
                return NotFound();
            }

            return View(sarcina);
        }

        // POST: Sarcinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sarcini == null)
            {
                return Problem("Entity set 'LibraryContext.Sarcini'  is null.");
            }
            var sarcina = await _context.Sarcini.FindAsync(id);
            if (sarcina != null)
            {
                _context.Sarcini.Remove(sarcina);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SarcinaExists(int id)
        {
          return (_context.Sarcini?.Any(e => e.SarcinaId == id)).GetValueOrDefault();
        }
    }
}
