using Domain.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioRegisterDTO>> GetAllRegistered();
        Task<Usuario> GetById(Guid usuarioId);
        Task<IEnumerable<Usuario>> GetByName(string nombre);
        Task<Usuario> Register(string nombre, string apellido, string username, string? fotoPerfil, string email, string password);
        Task<Usuario> UpdateUsuario(Guid usuarioId, string nombre, string apellido, string email, string fotoPerfil, string password);
        Task DeleteUsuario(Guid usuarioId);
        Task<UsuarioDTO> ValidateUserCredentials(UsuarioLoginDTO userLoginDto);

    }
}

