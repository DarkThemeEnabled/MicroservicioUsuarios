using Domain.DTO;

namespace Application.Interfaces
{
    public interface IComentarioApi
    {
        IEnumerable<ComentarioDTO> GetComentarios();
        ComentarioDTO GetComentarioById(int id);
    }
}
