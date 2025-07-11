using AduanasTec.Models;

namespace AduanasTec.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(Usuario usuario);
        Task<Usuario?> AuthenticateAsync(string username, string password);
    }
}
