using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuarioCommand
    {
        Task<Usuario> Register(string nombre, string apellido, string username, string email, string? fotoPerfil, string password);
        Task<Usuario> UpdateUsuario(Guid usuarioId, string nombre, string apellido, string email, string fotoPerfil, string password);
        Task DeleteUsuario(Guid usuarioId);
    }
}