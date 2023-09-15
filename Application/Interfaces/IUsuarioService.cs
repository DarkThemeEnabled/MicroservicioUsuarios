using Domain.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuarioService
    {
        Usuario CreateUser(UsuarioDto user);
        // IEnumerable<Usuario> getUser();
        // UserByIdDto getUserId(int id);
        void deleteUserId(int id);
        // List<UserByEmailDto> GetUserByEmail(string email);
        UsuarioDto Update(int id, UsuarioDto user);
        Usuario Authenticate(string username, string password);
    }
}
