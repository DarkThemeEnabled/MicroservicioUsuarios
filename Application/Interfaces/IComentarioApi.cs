using Domain.DTO;

namespace Application.Interfaces
{
    public interface IComentarioApi
    {
        Task<IEnumerable<ComentarioDTO>> GetComentarios();
        Task<ComentarioDTO> GetComentarioById(int id);
    }
}
