using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BarApp.Data;
using BarApp.Models;

namespace BarApp.Controllers
{
    [Authorize]
    public class PedidoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public PedidoController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Pedido
        public async Task<IActionResult> Index()
        {
            var pedidos = await _context.Pedido.Include(p => p.Empleado).ToListAsync();
            return View(pedidos);
        }

        // GET: Pedido/Completados
        public async Task<IActionResult> Ventas()
        {
            var pedidos = await _context.Pedido
                .Include(p => p.Empleado)
                .Where(p => p.Estado == PedidoEstado.Finalizado)
                .OrderByDescending(p => p.FechaCreacion)
                .ToListAsync();
            return View(pedidos);
        }

        // GET: Pedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Empleado)
                .Include(p => p.Items)
                    .ThenInclude(i => i.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedido/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.Productos = new MultiSelectList(_context.Producto, "Id", "Nombre");
            ViewBag.EmpleadoId = new SelectList(_context.Usuario, "Id", "UserName", user.Id);
            return View();
        }

        // POST: Pedido/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cliente,Mesa,FechaCreacion,Comentario,Estado,Productos,EmpleadoId")] Pedido pedido)
        {     
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                //await _context.SaveChangesAsync();
                var precioTotal = new decimal(0);

                foreach (var i in pedido.Productos) {
                    var producto = await _context.Producto.FindAsync(i);
                    if (producto.Stock == 0) {
                        ViewBag.Productos = new MultiSelectList(_context.Producto, "Id", "Nombre");
                        ViewBag.EmpleadoId = new SelectList(_context.Usuario, "Id", "UserName", user.Id);
                        ViewBag.Error = "No hay stock para " + producto.Nombre;
                        return View(pedido);
                    }
                    producto.Stock -= 1;
                    precioTotal += producto.Precio;
                    //await _context.SaveChangesAsync();
                    pedido.Items.Add(new PedidoItem() { PedidoId = pedido.Id, ProductoId = i });
                }

                pedido.PrecioTotal = precioTotal;
                
                await _context.SaveChangesAsync();

                return Redirect("/");
            }
            return View(pedido);
        }

        // GET: Pedido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            ViewBag.EmpleadoId = new SelectList(_context.Usuario, "Id", "UserName", user.Id);
            return View(pedido);
        }

        // POST: Pedido/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaCreacion,Cliente,Comentario,PrecioTotal,Estado,EmpleadoId")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
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
            return View(pedido);
        }

        // GET: Pedido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            _context.Pedido.Remove(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Pedido/Cerrar/5
        public async Task<IActionResult> Cerrar(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            pedido.Estado = PedidoEstado.Finalizado;
            await _context.SaveChangesAsync();
            return Redirect("/");
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(e => e.Id == id);
        }
    }
}
