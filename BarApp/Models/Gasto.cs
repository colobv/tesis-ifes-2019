using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarApp.Models
{
    public enum MetodoPago {
        Efectivo, Debito, Credito, MercadoPago
    }

    public class Gasto
    {
        public Gasto() {
            FechaCreacion = DateTime.Now;
        }

        public int Id { get; set; }

        public DateTime FechaCreacion { get; set; }

        [Required]
        public string Proveedor { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        
        public Categoria Categoria { get; set; }

        [EnumDataType(typeof(MetodoPago))]
        public MetodoPago MetodoPago { get; set; }

        [DisplayFormat(DataFormatString = "${0:0.##}")]
        [DataType(DataType.Currency)]
        public decimal Importe { get; set; }
        
        public string Comentario { get; set; }
    }
}