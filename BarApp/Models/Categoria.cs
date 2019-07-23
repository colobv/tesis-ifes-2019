using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BarApp.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        
        public List<Producto> Productos { get; set; }
    }
}