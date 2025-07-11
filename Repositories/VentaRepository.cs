using AduanasTec.Database;
using AduanasTec.Models;
using AduanasTec.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AduanasTec.Repositories
{
    public class VentaRepository : IVentaRepository
    {
        private readonly AppDbContext _context;

        public VentaRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Venta> CreateAsync(Venta venta)
        {
            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();
            return venta;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null) return false;

            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Venta>> GetAllAsync()
        {
            return await _context.Ventas
                           .Include(v => v.Cliente)
                           .Include(v => v.VentaProductos)
                               .ThenInclude(vp => vp.Producto)
                           .ToListAsync();
        }

        public async Task<Venta?> GetByIdAsync(int id)
        {

            return await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.VentaProductos)
                    .ThenInclude(vp => vp.Producto)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Venta?> UpdateAsync(Venta venta)
        {
            var existente = await _context.Ventas
                           .Include(v => v.VentaProductos)
                           .FirstOrDefaultAsync(v => v.Id == venta.Id);

            if (existente == null) return null;

            existente.Fecha = venta.Fecha;
            existente.ClienteId = venta.ClienteId;
            existente.Total = venta.Total;
            existente.VentaProductos = venta.VentaProductos;

            await _context.SaveChangesAsync();
            return existente;
        }
    }
}
