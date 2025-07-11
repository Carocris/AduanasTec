using AduanasTec.Models;
using AduanasTec.Repositories.Interfaces;
using AduanasTec.Services.Interfaces;

namespace AduanasTec.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Producto> CreateAsync(Producto producto)
        {
            return await _repository.CreateAsync(producto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async  Task<List<Producto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async  Task<Producto?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Producto?> UpdateAsync(Producto producto)
        {
            return await _repository.UpdateAsync(producto);
        }
    }
}
