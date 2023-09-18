using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;


namespace Infrastructure.Services
{
    public class UsuarioCommand : IUsuarioCommand
    {
        private readonly UsuarioContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioCommand(UsuarioContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Usuario> ActualizarUsuario(Guid usuarioId, string nombre, string apellido, string email, string fotoPerfil, string password)
        {
            // Crear un hash de la contraseña
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado.");
            }

            usuario.Nombre = nombre;
            usuario.Apellido = apellido;
            usuario.Email = email;
            usuario.FotoPerfil = fotoPerfil;
            if (usuario.PasswordHash != hashedPassword)  // Solo actualizamos la contraseña si ha cambiado
            {
                usuario.PasswordHash = hashedPassword;
            }

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }
        public async Task EliminarUsuario(Guid usuarioId)
        {
            // TODO: Implementar este método para obtener el ID del usuario autenticado
            var usuarioAutenticadoId = ObtenerUsuarioAutenticadoId();

            if (usuarioAutenticadoId != usuarioId)
            {
                throw new UnauthorizedAccessException("No tienes permiso para eliminar esta cuenta.");
            }

            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado.");
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }


        public async Task<Usuario> RegistrarUsuario(string nombre, string apellido, string email, string username, string fotoPerfil, string password)
        {
            // Verificar si el correo electrónico ya está en uso
            if (await _context.Usuarios.AnyAsync(u => u.Email == email))
            {
                throw new Exception("El correo electrónico ya está en uso.");
            }

            // Verificar si el nombre de usuario ya está en uso
            if (await _context.Usuarios.AnyAsync(u => u.Username == username))
            {
                throw new Exception("El nombre de usuario ya está en uso.");
            }
            // Crear un hash de la contraseña
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Asignar una foto de perfil por defecto si no se proporciona
            if (string.IsNullOrWhiteSpace(fotoPerfil))
            {
                fotoPerfil = "https://i.imgur.com/6QusTZ1.png"; // Nombre de archivo de imagen por defecto
            }

            var usuario = new Usuario
            {
                UsuarioId = Guid.NewGuid(),
                Nombre = nombre,
                Apellido = apellido,
                Username = username,
                Email = email,
                FotoPerfil = fotoPerfil, // Asignar la foto de perfil
                PasswordHash = hashedPassword // Almacenamos la contraseña hasheada
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        private Guid ObtenerUsuarioAutenticadoId()
        {
            var usuarioIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (usuarioIdClaim == null)
            {
                throw new UnauthorizedAccessException("No se pudo obtener el ID del usuario autenticado.");
            }

            return Guid.Parse(usuarioIdClaim.Value);
        }
    }
}