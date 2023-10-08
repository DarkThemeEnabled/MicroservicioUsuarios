using Application.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class UsuarioQuery : IUsuarioQuery
    {
        private readonly UsuarioContext _context;

        public UsuarioQuery(UsuarioContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioRegisterDTO>> GetAllRegistered()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return usuarios.Select(u => new UsuarioRegisterDTO
            {
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Email = u.Email,
                Username = u.Username,
                FotoPerfil = u.FotoPerfil,
                Password = u.PasswordHash
            }).ToList();
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
