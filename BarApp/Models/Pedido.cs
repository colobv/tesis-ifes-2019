using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarApp.Models
{
    public enum PedidoEstado {
        Pendiente, Entregado
    }
    
    public class Pedido
    {
        public int Id { get; set; }
        [Required]
        public string Cliente { get; set; }
        public string Comentario { get; set; }
        public IdentityUser Empleado { get; set; }
        [EnumDataType(typeof(PedidoEstado))]
        public PedidoEstado Estado { get; set; }
        public List<PedidoItem> PedidoItem { get; set; }
    }

    public class PedidoItem
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public Pedido Pedido { get; set; }
    }
}