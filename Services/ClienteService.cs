using AduanasTec.Models;
using AduanasTec.Repositories.Interfaces;
using AduanasTec.Services.Interfaces;

namespace AduanasTec.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }


        public async Task<Cliente> CreateAsync(Cliente cliente)
        {
            return await _repository.CreateAsync(cliente);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Cliente?> UpdateAsync(Cliente cliente)
        {
            return await _repository.UpdateAsync(cliente);
        }
    }
}
