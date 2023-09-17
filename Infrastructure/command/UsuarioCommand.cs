using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Services
{
    public class UsuarioCommand : IUsuarioCommand
    {
        private readonly UsuarioContext _context;

        public UsuarioCommand(UsuarioContext context)
        {
            _context = context;
        }

        public async Task<Usuario> RegistrarUsuario(string nombre, string apellido, string email, string fotoPerfil)
        {
            var usuario = new Usuario
            {
                UsuarioId = Guid.NewGuid(),
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                FotoPerfil = fotoPerfil
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> ActualizarUsuario(Guid usuarioId, string nombre, string apellido, string email, string fotoPerfil)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            usuario.Nombre = nombre;
            usuario.Apellido = apellido;
            usuario.Email = email;
            usuario.FotoPerfil = fotoPerfil;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task EliminarUsuario(Guid usuarioId)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
