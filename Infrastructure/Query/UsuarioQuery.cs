using Application.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Services
{
    public class UsuarioQuery : IUsuarioQuery
    {
        private readonly UsuarioContext _context;

        public UsuarioQuery(UsuarioContext context)
        {
            _context = context;
        }

        public Usuario UserLogin(string UserMail, string UserPassword)
        {
            var usuario = _context.Usuarios.Where(u => u.Email == UserMail && u.Password == UserPassword).FirstOrDefault();
            if (usuario == null) { return null; }

            return usuario;
        }

        public Usuario GetUsuarioById(Guid usuarioId)
        {
            var usuario = _context.Usuarios.FirstOrDefault(x => x.UsuarioId == usuarioId);
            return usuario;
        }

        public Usuario GetUsuarioByUsername(string username)
        {
            var usuario = _context.Usuarios.FirstOrDefault(x => x.Username == username);
            return usuario;
        }
        public List<Usuario> GetUsuarioList()
        {
            List<Usuario> usuarioList = _context.Usuarios.ToList();
            return usuarioList;
        }
    }
}
