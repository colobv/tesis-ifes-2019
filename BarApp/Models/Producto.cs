using System;
using System.ComponentModel.DataAnnotations;

namespace BarApp.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [DataType(DataType.Currency)]
        public decimal Costo { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public decimal Precio { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        
        public Categoria Categoria { get; set; }
    }
}