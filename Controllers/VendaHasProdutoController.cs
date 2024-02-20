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
    public class VendaHasProdutoController : Controller
    {
        private readonly Contexto _context;

        public VendaHasProdutoController(Contexto context)
        {
            _context = context;
        }

        // GET: VendaHasProduto
        public async Task<IActionResult> Index()
        {
            var contexto = _context.VendaHasProduto.Include(v => v.Prato).Include(v => v.Venda);
            return View(await contexto.ToListAsync());
        }

        // GET: VendaHasProduto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VendaHasProduto == null)
            {
                return NotFound();
            }

            var vendaHasProduto = await _context.VendaHasProduto
                .Include(v => v.Prato)
                .Include(v => v.Venda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendaHasProduto == null)
            {
                return NotFound();
            }

            return View(vendaHasProduto);
        }

        // GET: VendaHasProduto/Create
        public IActionResult Create()
        {
            ViewData["PratoId"] = new SelectList(_context.Prato, "Id", "NomePrato");
            ViewData["VendaId"] = new SelectList(_context.Venda, "Id", "Id");
            return View();
        }

        // POST: VendaHasProduto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VendaId,PratoId")] VendaHasProduto vendaHasProduto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendaHasProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PratoId"] = new SelectList(_context.Prato, "Id", "NomePrato", vendaHasProduto.PratoId);
            ViewData["VendaId"] = new SelectList(_context.Venda, "Id", "Id", vendaHasProduto.VendaId);
            return View(vendaHasProduto);
        }

        // GET: VendaHasProduto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VendaHasProduto == null)
            {
                return NotFound();
            }

            var vendaHasProduto = await _context.VendaHasProduto.FindAsync(id);
            if (vendaHasProduto == null)
            {
                return NotFound();
            }
            ViewData["PratoId"] = new SelectList(_context.Prato, "Id", "Id", vendaHasProduto.PratoId);
            ViewData["VendaId"] = new SelectList(_context.Venda, "Id", "Id", vendaHasProduto.VendaId);
            return View(vendaHasProduto);
        }

        // POST: VendaHasProduto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VendaId,PratoId")] VendaHasProduto vendaHasProduto)
        {
            if (id != vendaHasProduto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendaHasProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendaHasProdutoExists(vendaHasProduto.Id))
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
            ViewData["PratoId"] = new SelectList(_context.Prato, "Id", "NomePrato", vendaHasProduto.PratoId);
            ViewData["VendaId"] = new SelectList(_context.Venda, "Id", "Id", vendaHasProduto.VendaId);
            return View(vendaHasProduto);
        }

        // GET: VendaHasProduto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VendaHasProduto == null)
            {
                return NotFound();
            }

            var vendaHasProduto = await _context.VendaHasProduto
                .Include(v => v.Prato)
                .Include(v => v.Venda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendaHasProduto == null)
            {
                return NotFound();
            }

            return View(vendaHasProduto);
        }

        // POST: VendaHasProduto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VendaHasProduto == null)
            {
                return Problem("Entity set 'Contexto.VendaHasProduto'  is null.");
            }
            var vendaHasProduto = await _context.VendaHasProduto.FindAsync(id);
            if (vendaHasProduto != null)
            {
                _context.VendaHasProduto.Remove(vendaHasProduto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendaHasProdutoExists(int id)
        {
          return (_context.VendaHasProduto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
