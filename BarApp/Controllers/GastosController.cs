using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BarApp.Data;
using BarApp.Models;

namespace BarApp.Controllers
{
    [Authorize]
    public class GastosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GastosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Gastos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Gasto.Include(g => g.CategoriaGasto).Include(g => g.Proveedor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Gastos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasto = await _context.Gasto
                .Include(g => g.CategoriaGasto)
                .Include(g => g.Proveedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gasto == null)
            {
                return NotFound();
            }

            return View(gasto);
        }

        // GET: Gastos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaGastoId"] = new SelectList(_context.Set<CategoriaGasto>(), "Id", "Nombre");
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "Nombre");
            return View();
        }

        // POST: Gastos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaCreacion,CategoriaGastoId,ProveedorId,MetodoPago,Importe,Comentario")] Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaGastoId"] = new SelectList(_context.Set<CategoriaGasto>(), "Id", "Nombre", gasto.CategoriaGastoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "Nombre", gasto.ProveedorId);
            return View(gasto);
        }

        // GET: Gastos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasto = await _context.Gasto.FindAsync(id);
            if (gasto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaGastoId"] = new SelectList(_context.Set<CategoriaGasto>(), "Id", "Nombre", gasto.CategoriaGastoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "Nombre", gasto.ProveedorId);
            return View(gasto);
        }

        // POST: Gastos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaCreacion,CategoriaGastoId,ProveedorId,MetodoPago,Importe,Comentario")] Gasto gasto)
        {
            if (id != gasto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GastoExists(gasto.Id))
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
            ViewData["CategoriaGastoId"] = new SelectList(_context.Set<CategoriaGasto>(), "Id", "Nombre", gasto.CategoriaGastoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedor, "Id", "Nombre", gasto.ProveedorId);
            return View(gasto);
        }

        // GET: Gastos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasto = await _context.Gasto
                .Include(g => g.CategoriaGasto)
                .Include(g => g.Proveedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gasto == null)
            {
                return NotFound();
            }

            return View(gasto);
        }

        // POST: Gastos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gasto = await _context.Gasto.FindAsync(id);
            _context.Gasto.Remove(gasto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GastoExists(int id)
        {
            return _context.Gasto.Any(e => e.Id == id);
        }
    }
}
