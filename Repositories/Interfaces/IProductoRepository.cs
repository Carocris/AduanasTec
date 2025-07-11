using AduanasTec.Models;

namespace AduanasTec.Repositories.Interfaces
{
    public interface IProductoRepository
    {
        Task<List<Producto>> GetAllAsync();
        Task<Producto?> GetByIdAsync(int id);
        Task<Producto> CreateAsync(Producto producto);
        Task<Producto?> UpdateAsync(Producto producto);
        Task<bool> DeleteAsync(int id);
    }
}
