using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuarioCommand
    {
        Task<Usuario> RegistrarUsuario(string nombre, string apellido, string username, string email, string? fotoPerfil, string password);
        Task<Usuario> ActualizarUsuario(Guid usuarioId, string nombre, string apellido, string email, string fotoPerfil, string password);
        Task EliminarUsuario(Guid usuarioId);
    }
}