using Domain.DTO;

namespace Application.Interfaces
{
    public interface IUserComentarioService
    {
        ComentarioDTO GetByID(int id);
        //dynamic GetComentarios();
        string GetComentarioReceta(int id);
        void CreateComentario(ComentarioDTO comentarioData, string userToken);
    }
}