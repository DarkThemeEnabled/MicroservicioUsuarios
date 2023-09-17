using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuarioCommand
    {
        Task<Usuario> RegistrarUsuario(string nombre, string apellido, string email, string fotoPerfil);
        Task<Usuario> ActualizarUsuario(Guid usuarioId, string nombre, string apellido, string email, string fotoPerfil);
        Task EliminarUsuario(Guid usuarioId);
    }
}