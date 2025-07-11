using AduanasTec.Models;
using AduanasTec.Repositories.Interfaces;
using AduanasTec.Services.Interfaces;

namespace AduanasTec.Services
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _repository;

        public VentaService(IVentaRepository repository)
        {
            _repository = repository;
        }
        public async Task<Venta> CreateAsync(Venta venta)
        {
            return await _repository.CreateAsync(venta);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<Venta>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Venta?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Venta?> UpdateAsync(Venta venta)
        {
            return await _repository.UpdateAsync(venta);
        }
    }
}
