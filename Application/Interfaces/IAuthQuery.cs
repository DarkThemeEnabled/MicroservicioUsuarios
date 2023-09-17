using Domain.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthQuery
    {
        Task<UsuarioDTO> ObtenerUsuarioPorToken(string token);
        Task<Usuario> GetUsuarioByEmail(string email);
    }
}
