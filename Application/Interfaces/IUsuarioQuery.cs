using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuarioQuery
    {
        List<Usuario> GetUsuarioList();
        Usuario GetUsuarioById(Guid usuarioId);
        Usuario GetUsuarioByUsername(string username);
        Usuario UserLogin(string UserMail, string UserPassword);
    }
}