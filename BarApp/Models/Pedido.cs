using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarApp.Models
{
    public enum PedidoEstado {
        Pendiente, Entregado
    }
    
    public class Pedido
    {
        public Pedido() {
            Items = new List<PedidoItem>();
            FechaCreacion = DateTime.Now;
            PrecioTotal = 0;
        }
        
        public int Id { get; set; }

        public DateTime FechaCreacion { get; set; }

        [Required]
        public string Cliente { get; set; }

        public string Comentario { get; set; }

        public Usuario Empleado { get; set; }
        
        [Display(Name = "Empleado")]
        public string EmpleadoId { get; set; }

        [EnumDataType(typeof(PedidoEstado))]
        public PedidoEstado Estado { get; set; }

        public List<PedidoItem> Items { get; set; }

        [DisplayFormat(DataFormatString = "${0:0.##}")]
        [DataType(DataType.Currency)]
        public decimal PrecioTotal { get; set; }

        [NotMapped]
        public int[] Productos { get; set; }
    }

    public class PedidoItem
    {
        private const int CANTIDAD_DEFAULT = 1;

        public int Id { get; set; }

        public int PedidoId { get; set; }
        
        public int ProductoId { get; set; }

        [DefaultValue(CANTIDAD_DEFAULT)]
        public int Cantidad { get; set; }

        public Producto Producto { get; set; }

        public Pedido Pedido { get; set; }
    }
}