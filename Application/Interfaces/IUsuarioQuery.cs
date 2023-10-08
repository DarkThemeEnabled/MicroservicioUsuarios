using Domain.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuarioQuery
    {
        Task<IEnumerable<UsuarioRegisterDTO>> GetAllRegistered();
        Task<Usuario> GetById(Guid usuarioId);
        Task<IEnumerable<Usuario>> GetByName(string nombre);
    }
}