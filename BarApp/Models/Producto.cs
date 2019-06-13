using System;
using System.ComponentModel.DataAnnotations;

namespace BarApp.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }
        public decimal Precio { get; set; }
    }
}