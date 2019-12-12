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
    public class CategoriaGastoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriaGastoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CategoriaGasto
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoriaGasto.ToListAsync());
        }

        // GET: CategoriaGasto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaGasto = await _context.CategoriaGasto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaGasto == null)
            {
                return NotFound();
            }

            return View(categoriaGasto);
        }

        // GET: CategoriaGasto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriaGasto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] CategoriaGasto categoriaGasto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaGasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaGasto);
        }

        // GET: CategoriaGasto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaGasto = await _context.CategoriaGasto.FindAsync(id);
            if (categoriaGasto == null)
            {
                return NotFound();
            }
            return View(categoriaGasto);
        }

        // POST: CategoriaGasto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] CategoriaGasto categoriaGasto)
        {
            if (id != categoriaGasto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaGasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaGastoExists(categoriaGasto.Id))
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
            return View(categoriaGasto);
        }

        // GET: CategoriaGasto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaGasto = await _context.CategoriaGasto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaGasto == null)
            {
                return NotFound();
            }

            return View(categoriaGasto);
        }

        // POST: CategoriaGasto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriaGasto = await _context.CategoriaGasto.FindAsync(id);
            _context.CategoriaGasto.Remove(categoriaGasto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaGastoExists(int id)
        {
            return _context.CategoriaGasto.Any(e => e.Id == id);
        }
    }
}
