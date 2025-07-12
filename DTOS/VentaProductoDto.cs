using System.ComponentModel.DataAnnotations;

namespace AduanasTec.DTOS
{
    public class VentaProductoDto
    {
        [Required]
        public int ProductoId { get; set; }

        [Required]
        [Range(1, 9999, ErrorMessage = "La cantidad debe ser entre 1 y 9999")]
        public int Cantidad { get; set; }
    }
}
