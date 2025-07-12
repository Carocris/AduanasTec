using AduanasTec.Controllers;
using System.ComponentModel.DataAnnotations;

namespace AduanasTec.DTOS
{
    public class CreateVentaDto
    {
        [Required]
        public int ClienteId { get; set; }

        [Required]
        public List<VentaProductoDto> VentaProductos { get; set; } = new();

        [Required]
        public decimal Total { get; set; }
    }
}
