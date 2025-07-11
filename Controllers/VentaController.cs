using Microsoft.AspNetCore.Mvc;
using AduanasTec.Models;
using AduanasTec.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace AduanasTec.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ventas = await _ventaService.GetAllAsync();
            return Ok(ventas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var venta = await _ventaService.GetByIdAsync(id);
            if (venta == null) return NotFound();
            return Ok(venta);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Venta venta)
        {
            var creada = await _ventaService.CreateAsync(venta);
            return CreatedAtAction(nameof(GetById), new { id = creada.Id }, creada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Venta venta)
        {
            if (id != venta.Id) return BadRequest();

            var actualizada = await _ventaService.UpdateAsync(venta);
            if (actualizada == null) return NotFound();

            return Ok(actualizada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _ventaService.DeleteAsync(id);
            if (!eliminado) return NotFound();

            return NoContent();
        }
    }
}
