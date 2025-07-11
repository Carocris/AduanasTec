using System.ComponentModel.DataAnnotations.Schema;

namespace AduanasTec.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        // ✅ Relación con Cliente (uno a muchos)
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        // ✅ Relación muchos a muchos con productos
        public List<VentaProducto> VentaProductos { get; set; } = new();

        public decimal Total { get; set; }
    }
}
