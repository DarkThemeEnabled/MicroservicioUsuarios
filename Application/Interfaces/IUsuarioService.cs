using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuarioService
    {
        UsuarioTokenResponse Authenticacion(UsuarioLoginRequest request);
        UsuarioResponse CreateUsuario(UsuarioPasswordRequest request);
        UsuarioDeleteResponse RemoveUsuario(Guid usuarioId);
        UsuarioResponse UpdateUsuario(Guid usuarioId, UsuarioRequest request);
        List<Usuario> GetUsuarioList();
        UsuarioResponse GetUsuarioById(Guid usuarioId);
    }
}

