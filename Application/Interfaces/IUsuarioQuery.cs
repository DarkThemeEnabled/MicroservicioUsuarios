using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuarioQuery
    {
        List<Usuario> GetUsuarioList();
        Usuario GetUsuarioById(Guid usuarioId);
        Usuario UserLogin(string UserMail, string UserPassword);
    }
}