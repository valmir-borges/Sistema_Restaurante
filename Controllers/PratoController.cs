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
    public class PratoController : Controller
    {
        private readonly Contexto _context;

        public PratoController(Contexto context)
        {
            _context = context;
        }

        // GET: Prato
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Prato.Include(p => p.Categoria).Include(p => p.Fornecedor);
            return View(await contexto.ToListAsync());
        }

        // GET: Prato/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prato == null)
            {
                return NotFound();
            }

            var prato = await _context.Prato
                .Include(p => p.Categoria)
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prato == null)
            {
                return NotFound();
            }

            return View(prato);
        }

        // GET: Prato/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "NomeCategia");
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedor, "Id", "NomeFornecedor");
            return View();
        }

        // POST: Prato/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomePrato,PrecoPrato,CategoriaId,FornecedorId")] Prato prato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "NomeCategia", prato.CategoriaId);
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedor, "Id", "NomeFornecedor", prato.FornecedorId);
            return View(prato);
        }

        // GET: Prato/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prato == null)
            {
                return NotFound();
            }

            var prato = await _context.Prato.FindAsync(id);
            if (prato == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "NomeCategia", prato.CategoriaId);
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedor, "Id", "NomeFornecedor", prato.FornecedorId);
            return View(prato);
        }

        // POST: Prato/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomePrato,PrecoPrato,CategoriaId,FornecedorId")] Prato prato)
        {
            if (id != prato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PratoExists(prato.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "NomeCategia", prato.CategoriaId);
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedor, "Id", "NomeFornecedor", prato.FornecedorId);
            return View(prato);
        }

        // GET: Prato/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prato == null)
            {
                return NotFound();
            }

            var prato = await _context.Prato
                .Include(p => p.Categoria)
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prato == null)
            {
                return NotFound();
            }

            return View(prato);
        }

        // POST: Prato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prato == null)
            {
                return Problem("Entity set 'Contexto.Prato'  is null.");
            }
            var prato = await _context.Prato.FindAsync(id);
            if (prato != null)
            {
                _context.Prato.Remove(prato);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PratoExists(int id)
        {
          return (_context.Prato?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
