using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuarioQuery
    {
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario> GetById(Guid usuarioId);
        Task<IEnumerable<Usuario>> GetByName(string nombre);
    }
}
