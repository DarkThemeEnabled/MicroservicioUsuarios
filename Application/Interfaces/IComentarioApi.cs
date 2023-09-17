using Domain.DTO;

namespace Application.Interfaces
{
    public interface IComentarioApi
    {
        Task<IEnumerable<ComentarioDTO>> GetComentarios();
        Task<ComentarioDTO> GetComentarioById(int id);
        Task<bool> CreateComentario(ComentarioDTO comentario);
        Task<bool> UpdateComentario(ComentarioDTO comentario);
        Task<bool> DeleteComentario(int id);
    }
}
