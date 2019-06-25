using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BarApp.Models;

namespace BarApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
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
    }
}
