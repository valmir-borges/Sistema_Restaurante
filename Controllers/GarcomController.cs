using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema_Restaurante.Models;

namespace Sistema_Restaurante.Controllers
{
    public class GarcomController : Controller
    {
        private readonly Contexto _context;

        public GarcomController(Contexto context)
        {
            _context = context;
        }

        // GET: Garcom
        public async Task<IActionResult> Index()
        {
              return _context.Garcom != null ? 
                          View(await _context.Garcom.ToListAsync()) :
                          Problem("Entity set 'Contexto.Garcom'  is null.");
        }

        // GET: Garcom/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Garcom == null)
            {
                return NotFound();
            }

            var garcom = await _context.Garcom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (garcom == null)
            {
                return NotFound();
            }

            return View(garcom);
        }

        // GET: Garcom/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Garcom/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeGarcom,IdadeGarcom")] Garcom garcom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(garcom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(garcom);
        }

        // GET: Garcom/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Garcom == null)
            {
                return NotFound();
            }

            var garcom = await _context.Garcom.FindAsync(id);
            if (garcom == null)
            {
                return NotFound();
            }
            return View(garcom);
        }

        // POST: Garcom/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeGarcom,IdadeGarcom")] Garcom garcom)
        {
            if (id != garcom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(garcom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GarcomExists(garcom.Id))
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
            return View(garcom);
        }

        // GET: Garcom/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Garcom == null)
            {
                return NotFound();
            }

            var garcom = await _context.Garcom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (garcom == null)
            {
                return NotFound();
            }

            return View(garcom);
        }

        // POST: Garcom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Garcom == null)
            {
                return Problem("Entity set 'Contexto.Garcom'  is null.");
            }
            var garcom = await _context.Garcom.FindAsync(id);
            if (garcom != null)
            {
                _context.Garcom.Remove(garcom);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GarcomExists(int id)
        {
          return (_context.Garcom?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
