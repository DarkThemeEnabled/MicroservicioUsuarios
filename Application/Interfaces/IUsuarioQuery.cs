using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuarioQuery
    {
        Task<IEnumerable<Usuario>> ObtenerTodosLosUsuarios();
        Task<Usuario?> ObtenerUsuarioPorId(Guid usuarioId);
        Task<IEnumerable<Usuario>> ObtenerUsuariosPorNombre(string nombre);
    }
}
