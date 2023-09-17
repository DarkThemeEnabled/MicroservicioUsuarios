using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> ObtenerTodosLosUsuarios();
        Task<Usuario?> ObtenerUsuarioPorId(Guid usuarioId);
        Task<IEnumerable<Usuario>> ObtenerUsuariosPorNombre(string nombre);
        Task<Usuario> RegistrarUsuario(string nombre, string apellido, string email, string fotoPerfil);
        Task<Usuario> ActualizarUsuario(Guid usuarioId, string nombre, string apellido, string email, string fotoPerfil);
        Task EliminarUsuario(Guid usuarioId);
    }
}

