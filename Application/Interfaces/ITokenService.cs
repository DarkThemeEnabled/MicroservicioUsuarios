using Application.Response;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        UsuarioTokenResponse GenerateToken(Usuario userLogin);
        bool IsTokenExpired(string token);
    }
}
