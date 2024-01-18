using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Niculae_Ana_Maria_Proiect3.Data;
using Niculae_Ana_Maria_Proiect3.Models;
using Niculae_Ana_Maria_Proiect3.Models.View;

namespace Niculae_Ana_Maria_Proiect3.Controllers
{
    public class SarciniController : Controller
    {
        private readonly LibraryContext _context;

        public SarciniController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Sarcini
        public IActionResult Index()
        {
            var sarcini = _context.Sarcini
                .Include(s => s.ProiectAsociat)
                    .ThenInclude(p => p.ManagerProiect)
                .Include(s => s.SarcinaMembriEchipa)
                    .ThenInclude(sme => sme.MembruEchipa)
                .ToList();

            return View(sarcini);
        }


        // GET: Sarcini/Details/5
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

        //public IActionResult Create()
        //{
        //    ViewData["ProiectId"] = new SelectList(_context.Proiecte, "ProiectId", "Nume");
        //    ViewData["MembriEchipa"] = new SelectList(_context.MembriEchipa, "MembruEchipaId", "Nume");
        //    return View();
        //}
        public IActionResult Create()
        {
            ViewData["ProiectId"] = new SelectList(_context.Proiecte, "ProiectId", "Nume");
            ViewData["MembriEchipa"] = new SelectList(_context.MembriEchipa, "MembruEchipaId", "Nume");

            var viewModel = new SarcinaViewModel
            {
                MembriiEchipa = _context.MembriEchipa
                    .Select(member => new MembruEchipaCheckboxViewModel
                    {
                        MembruEchipaId = member.MembruEchipaId,
                        Nume = member.Nume,
                        IsSelected = false // Initialize all checkboxes as unchecked
                    })
                    .ToList()
            };

            return View(viewModel);
        }


        // POST: Sarcini/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("NumeSarcina,Descriere,DataIncepere,DataFinalizare,Status,ProiectId")] Sarcina sarcina, int[] SelectedMembriEchipaIds)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Sarcini.Add(sarcina);

        //        if (SelectedMembriEchipaIds != null)
        //        {
        //            foreach (var membruId in SelectedMembriEchipaIds)
        //            {
        //                _context.SarcinaMembriEchipa.Add(new SarcinaMembruEchipa { SarcinaId = sarcina.SarcinaId, MembruEchipaId = membruId });
        //            }
        //        }

        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    foreach (var key in ModelState.Keys)
        //    {
        //        var modelStateEntry = ModelState[key];
        //        foreach (var error in modelStateEntry.Errors)
        //        {
        //            // Log or print the error messages to diagnose the issue
        //            Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
        //        }
        //    }
        //    ViewData["ProiectId"] = new SelectList(_context.Proiecte, "ProiectId", "Nume", sarcina.ProiectId);
        //    ViewData["MembriEchipa"] = new SelectList(_context.MembriEchipa, "MembruEchipaId", "Nume");
        //    ViewData["Status"] = new SelectList(Enum.GetValues(typeof(StatusSarcina)));
        //    return View(sarcina);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SarcinaViewModel viewModel)
        {
            var sarcina = new Sarcina
            {
                NumeSarcina = viewModel.Sarcina.NumeSarcina,
                Descriere = viewModel.Sarcina.Descriere,
                DataIncepere = viewModel.Sarcina.DataIncepere,
                DataFinalizare = viewModel.Sarcina.DataFinalizare,
                Status = viewModel.Sarcina.Status,
                ProiectId = viewModel.Sarcina.ProiectId
            };

            _context.Sarcini.Add(sarcina);
            await _context.SaveChangesAsync();  // Save Sarcina to get its generated ID

            if (viewModel.MembriiEchipa != null)
            {
                foreach (var member in viewModel.MembriiEchipa)
                {
                    if (member.IsSelected)
                    {
                        _context.SarcinaMembriEchipa.Add(new SarcinaMembruEchipa
                        {
                            SarcinaId = sarcina.SarcinaId, // Now sarcina.SarcinaId has a value
                            MembruEchipaId = member.MembruEchipaId
                        });
                    }
                }
                await _context.SaveChangesAsync();  // Save SarcinaMembruEchipa entities
            }

            return RedirectToAction(nameof(Index));
        }






        // GET: Sarcini/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sarcina = await _context.Sarcini
                .Include(s => s.SarcinaMembriEchipa)
                .FirstOrDefaultAsync(s => s.SarcinaId == id);

            if (sarcina == null)
            {
                return NotFound();
            }

            // Populate the ViewBag with team members' names and IDs
            ViewBag.TeamMembers = new MultiSelectList(_context.MembriEchipa, "MembruEchipaId", "NumeMembruEchipa", sarcina.SarcinaMembriEchipa.Select(sm => sm.MembruEchipaId));

            // Store the selected team members' IDs in a temporary field
            ViewBag.SelectedTeamMembers = sarcina.SarcinaMembriEchipa.Select(sm => sm.MembruEchipaId).ToArray();

            return View(sarcina);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sarcina sarcina, int[] selectedMembers)
        {
            if (id != sarcina.SarcinaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update the selected team members based on the IDs in selectedMembers
                    sarcina.SarcinaMembriEchipa = selectedMembers
                        .Select(membruId => new SarcinaMembruEchipa { SarcinaId = sarcina.SarcinaId, MembruEchipaId = membruId })
                        .ToList();

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

            foreach (var key in ModelState.Keys)
            {
                var modelStateEntry = ModelState[key];
                foreach (var error in modelStateEntry.Errors)
                {
                    // Log or print the error messages to diagnose the issue
                    Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                }
            }

            // Repopulate the ViewBag with team members' names and IDs
            ViewBag.TeamMembers = new MultiSelectList(_context.MembriEchipa, "MembruEchipaId", "NumeMembruEchipa", sarcina.SarcinaMembriEchipa.Select(sm => sm.MembruEchipaId));

            // Store the selected team members' IDs in a temporary field
            ViewBag.SelectedTeamMembers = selectedMembers;

            return View(sarcina);
        }





        // GET: Sarcini/Delete/5
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

        // POST: Sarcini/Delete/5
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
