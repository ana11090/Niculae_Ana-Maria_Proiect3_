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
            if (id == null)
            {
                return NotFound();
            }

            var sarcina = await _context.Sarcini
                .Include(s => s.ProiectAsociat)
                .Include(s => s.SarcinaMembriEchipa).ThenInclude(sme => sme.MembruEchipa)
                .FirstOrDefaultAsync(m => m.SarcinaId == id);

            if (sarcina == null)
            {
                return NotFound();
            }

            // If you're using IdentityDbContext with ApplicationUser
            var commentsWithUsernames = await _context.Comentarii
     .Where(c => c.SarcinaId == id)
     .OrderBy(c => c.DataOra)
     .Select(c => new
     {
         c.Text,
         c.DataOra,
         Username = _context.Users.FirstOrDefault(u => u.Id == c.AutorId).UserName
     })
     .ToListAsync();


            // Check if the result is not null or empty
            if (commentsWithUsernames == null || !commentsWithUsernames.Any())
            {
                Console.WriteLine("No comments found or query failed");
            }

            ViewBag.Comments = commentsWithUsernames;

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

            // Fetch the IDs of associated team members
            var teamMemberIds = sarcina.SarcinaMembriEchipa.Select(sme => sme.MembruEchipaId).ToList();

            var viewModel = new SarcinaViewModel
            {
                Sarcina = sarcina,
                MembriiEchipa = _context.MembriEchipa
                    .Select(me => new MembruEchipaCheckboxViewModel
                    {
                        MembruEchipaId = me.MembruEchipaId,
                        Nume = me.Nume,
                        IsSelected = teamMemberIds.Contains(me.MembruEchipaId)
                    })
                    .ToList()
            };

            ViewData["ProiectId"] = new SelectList(_context.Proiecte, "ProiectId", "Nume", sarcina.ProiectId);
            ViewData["Status"] = new SelectList(Enum.GetValues(typeof(StatusSarcina)), sarcina.Status);

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SarcinaViewModel viewModel)
        {
            if (id != viewModel.Sarcina.SarcinaId)
            {
                return NotFound();
            }

            var sarcinaToUpdate = await _context.Sarcini
                .Include(s => s.SarcinaMembriEchipa)
                .FirstOrDefaultAsync(s => s.SarcinaId == id);

            if (sarcinaToUpdate == null)
            {
                return NotFound();
            }

            
                // Update the properties of the sarcina
                sarcinaToUpdate.NumeSarcina = viewModel.Sarcina.NumeSarcina;
                sarcinaToUpdate.Descriere = viewModel.Sarcina.Descriere;
                // ... other properties ...

                // Update team members
                var selectedMembers = viewModel.MembriiEchipa
                    .Where(m => m.IsSelected)
                    .Select(m => m.MembruEchipaId)
                    .ToList();

                sarcinaToUpdate.SarcinaMembriEchipa.Clear();
                foreach (var memberId in selectedMembers)
                {
                    sarcinaToUpdate.SarcinaMembriEchipa.Add(new SarcinaMembruEchipa
                    {
                        SarcinaId = id,
                        MembruEchipaId = memberId
                    });
                }

                _context.Update(sarcinaToUpdate);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            

            //// Handle invalid model state
            //ViewData["ProiectId"] = new SelectList(_context.Proiecte, "ProiectId", "Nume", viewModel.Sarcina.ProiectId);
            //ViewData["Status"] = new SelectList(Enum.GetValues(typeof(StatusSarcina)), viewModel.Sarcina.Status);

            //// Repopulate team members for the view model
            //viewModel.MembriiEchipa = _context.MembriEchipa.Select(me => new MembruEchipaCheckboxViewModel
            //{
            //    MembruEchipaId = me.MembruEchipaId,
            //    Nume = me.Nume,
            //    IsSelected = selectedMembers.Contains(me.MembruEchipaId)
            //}).ToList();

            //return View(viewModel);
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
