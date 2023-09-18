using Application.Request;
using Application.Response;

namespace Application.Interfaces
{
    public interface IAuthCommand
    {
        Task<AuthResponse> Registrar(RegisterRequest request);
        Task<AuthResponse> Login(LoginRequest request);
    }
}