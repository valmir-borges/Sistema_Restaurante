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
    public class VendaController : Controller
    {
        private readonly Contexto _context;

        public VendaController(Contexto context)
        {
            _context = context;
        }

        // GET: Venda
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Venda.Include(v => v.Cliente).Include(v => v.Garcom).Include(v => v.Pagamento);
            return View(await contexto.ToListAsync());
        }

        // GET: Venda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Venda == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda
                .Include(v => v.Cliente)
                .Include(v => v.Garcom)
                .Include(v => v.Pagamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // GET: Venda/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "NomeCliente");
            ViewData["GarcomId"] = new SelectList(_context.Garcom, "Id", "NomeGarcom");
            ViewData["PagamentoId"] = new SelectList(_context.Pagamento, "Id", "FormaPagamento");
            return View();
        }

        // POST: Venda/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ValorTotal,Data,ClienteId,GarcomId,PagamentoId")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "NomeCliente", venda.ClienteId);
            ViewData["GarcomId"] = new SelectList(_context.Garcom, "Id", "NomeGarcom", venda.GarcomId);
            ViewData["PagamentoId"] = new SelectList(_context.Pagamento, "Id", "FormaPagamento", venda.PagamentoId);
            return View(venda);
        }

        // GET: Venda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Venda == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "NomeCliente", venda.ClienteId);
            ViewData["GarcomId"] = new SelectList(_context.Garcom, "Id", "NomeGarcom", venda.GarcomId);
            ViewData["PagamentoId"] = new SelectList(_context.Pagamento, "Id", "FormaPagamento", venda.PagamentoId);
            return View(venda);
        }

        // POST: Venda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ValorTotal,Data,ClienteId,GarcomId,PagamentoId")] Venda venda)
        {
            if (id != venda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendaExists(venda.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "NomeCliente", venda.ClienteId);
            ViewData["GarcomId"] = new SelectList(_context.Garcom, "Id", "NomeGarcom", venda.GarcomId);
            ViewData["PagamentoId"] = new SelectList(_context.Pagamento, "Id", "FormaPagamento", venda.PagamentoId);
            return View(venda);
        }

        // GET: Venda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Venda == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda
                .Include(v => v.Cliente)
                .Include(v => v.Garcom)
                .Include(v => v.Pagamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // POST: Venda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Venda == null)
            {
                return Problem("Entity set 'Contexto.Venda'  is null.");
            }
            var venda = await _context.Venda.FindAsync(id);
            if (venda != null)
            {
                _context.Venda.Remove(venda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendaExists(int id)
        {
          return (_context.Venda?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        // GET: Venda/Details/5
        public async Task<IActionResult> Impressao(int? id)
        {
            if (id == null || _context.Venda == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda
                .Include(v => v.Cliente)
                .Include(v => v.Pagamento)
                .Include(v => v.Garcom)
                .FirstOrDefaultAsync(m => m.Id == id);
            venda.ProdutoList = await _context.VendaHasProduto
            //Busca os dados da outra tabela
            .Include(x => x.Prato)
            //Seleciona apenas os produtos daquela venda
            .Where(x => x.VendaId == id)
            //Agrupa os produtos iguais
            .GroupBy(x => new { x.PratoId })
            //Seleciona os dados do agrupamento
            .Select(vp => vp.OrderByDescending(x => x.PratoId).First())
            //Converta tudo isso em lista
            .ToListAsync();
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }
    }
}
