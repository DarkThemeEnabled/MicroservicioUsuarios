using Application.Request;

namespace Application.Interfaces
{
    public interface IAuthCommand
    {
        Task<AuthRequest> Registrar(RegisterRequest request);
        Task<AuthRequest> Login(LoginRequest request);
    }
}