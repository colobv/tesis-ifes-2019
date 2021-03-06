using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BarApp.Models;

namespace BarApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Producto>()
        //         .Property(p => p.Costo)
        //         .HasDefaultValue(0);
        // }

        public DbSet<BarApp.Models.Producto> Producto { get; set; }
        public DbSet<BarApp.Models.Categoria> Categoria { get; set; }
        public DbSet<BarApp.Models.Pedido> Pedido { get; set; }
        public DbSet<BarApp.Models.Usuario> Usuario { get; set; }
        public DbSet<BarApp.Models.Gasto> Gasto { get; set; }
        public DbSet<BarApp.Models.Proveedor> Proveedor { get; set; }
        public DbSet<BarApp.Models.CategoriaGasto> CategoriaGasto { get; set; }
    }
}
