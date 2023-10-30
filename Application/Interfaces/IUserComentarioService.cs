using Domain.DTO;

namespace Application.Interfaces
{
    public interface IUserComentarioService
    {
        dynamic GetByID(int id);
        dynamic GetComentarios();
        void CreateComentario(ComentarioDTO comentarioData, string userToken);
    }
}