using AduanasTec.Models;

namespace AduanasTec.Repositories.Interfaces
{
    public interface IVentaRepository
    {
        Task<List<Venta>> GetAllAsync();
        Task<Venta?> GetByIdAsync(int id);
        Task<Venta> CreateAsync(Venta venta);
        Task<Venta?> UpdateAsync(Venta venta);
        Task<bool> DeleteAsync(int id);
    }
}
