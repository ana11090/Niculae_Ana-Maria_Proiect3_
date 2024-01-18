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
    public class MembriiEchipaController : Controller
    {
        private readonly LibraryContext _context;

        public MembriiEchipaController(LibraryContext context)
        {
            _context = context;
        }

        // GET: MembriiEchipa
        public async Task<IActionResult> Index()
        {
            var membriEchipa = await _context.MembriEchipa.Include(m => m.Manager).ToListAsync();
            return View(membriEchipa); 
        }



        // GET: MembriiEchipa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MembriEchipa == null)
            {
                return NotFound();
            }

            var membruEchipa = await _context.MembriEchipa
                .Include(m => m.Manager)
                .FirstOrDefaultAsync(m => m.MembruEchipaId == id);
            if (membruEchipa == null)
            {
                return NotFound();
            }

            return View(membruEchipa);
        }

        // GET: MembriiEchipa/Create
        public IActionResult Create()
        {
            ViewData["ManagerId"] = new SelectList(_context.Manageri, "ManagerId", "Nume");
            return View();
        }

        // POST: MembriiEchipa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MembruEchipaId,Nume,Functie,ManagerId")] MembruEchipa membruEchipa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membruEchipa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManagerId"] = new SelectList(_context.Manageri, "ManagerId", "Nume", membruEchipa.ManagerId); 
            return View(membruEchipa);
        }


        // GET: MembriiEchipa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membruEchipa = await _context.MembriEchipa.FindAsync(id);
            if (membruEchipa == null)
            {
                return NotFound();
            }

            ViewData["ManagerId"] = new SelectList(_context.Manageri, "ManagerId", "Nume", membruEchipa.ManagerId);
            return View(membruEchipa);
        }

        // POST: MembriiEchipa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MembruEchipaId,Nume,Functie,ManagerId")] MembruEchipa membruEchipa)
        {
            if (id != membruEchipa.MembruEchipaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membruEchipa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembruEchipaExists(membruEchipa.MembruEchipaId))
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
            ViewData["ManagerId"] = new SelectList(_context.Manageri, "ManagerId", "Nume", membruEchipa.ManagerId);
            return View(membruEchipa);
        }

        // GET: MembriiEchipa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membruEchipa = await _context.MembriEchipa
                .Include(m => m.Manager)
                .FirstOrDefaultAsync(m => m.MembruEchipaId == id);
            if (membruEchipa == null)
            {
                return NotFound();
            }

            return View(membruEchipa);
        }

        // POST: MembriiEchipa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membruEchipa = await _context.MembriEchipa.FindAsync(id);
            if (membruEchipa != null)
            {
                _context.MembriEchipa.Remove(membruEchipa);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MembruEchipaExists(int id)
        {
          return (_context.MembriEchipa?.Any(e => e.MembruEchipaId == id)).GetValueOrDefault();
        }
    }
}
