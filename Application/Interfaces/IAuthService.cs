using Domain.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(string email, string password);
        Task<string> Login(string email, string password);
        Task<UsuarioDTO> ObtenerUsuarioPorToken(string token);
        Task<Usuario> GetUsuarioByEmail(string email);
    }
}
