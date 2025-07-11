using AduanasTec.Database;
using AduanasTec.Models;
using AduanasTec.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AduanasTec.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _context;

        public ProductoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Producto> CreateAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Producto>> GetAllAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto?> GetByIdAsync(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public  async Task<Producto?> UpdateAsync(Producto producto)
        {
            var productoexistente = await _context.Productos.FindAsync(producto.Id);
            if (productoexistente == null) return null;

            productoexistente.Nombre = producto.Nombre;
            productoexistente.Descripcion = producto.Descripcion;
            productoexistente.Precio = producto.Precio;
            productoexistente.Stock = producto.Stock;

            await _context.SaveChangesAsync();
            return productoexistente;
        }
    }
}
