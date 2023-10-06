using Application.Interfaces;
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

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await Task.FromResult(_context.Usuarios.ToList());
        }

        public async Task<Usuario> GetById(Guid usuarioId)
        {
            return await _context.Usuarios.FindAsync(usuarioId);
        }

        public async Task<IEnumerable<Usuario>> GetByName(string nombre)
        {
            return await Task.FromResult(_context.Usuarios.Where(u => u.Nombre.Contains(nombre)).ToList());
        }
    }
}
