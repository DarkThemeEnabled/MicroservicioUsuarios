using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Domain.Entities;
using Infrastructure.Persistence;


namespace Infrastructure.Command
{
    public class UsuarioCommand : IUsuarioCommand
    {
        private readonly UsuarioContext _context;

        public UsuarioCommand(UsuarioContext context)
        {
            _context = context;
        }

        public Usuario CreateUsuario(Usuario usuario)
        {
            // Verificar si los campos obligatorios están vacíos o nulos
            if (string.IsNullOrWhiteSpace(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Username))
            {
                throw new InvalidDataException("El correo electrónico y el nombre de usuario son obligatorios.");
            }

            var existMail = _context.Usuarios.FirstOrDefault(u => u.Email == usuario.Email);
            if (existMail != null)
            {
                throw new ExistingMailException("El correo electrónico ya está en uso.");
            }

            var existUsername = _context.Usuarios.FirstOrDefault(u => u.Username == usuario.Username);
            if (existUsername != null)
            {
                throw new ExistingUsernameException("El nombre de usuario ya está en uso.");
            }

            if (string.IsNullOrEmpty(usuario.FotoPerfil))
            {
                usuario.FotoPerfil = "https://i.imgur.com/6QusTZ1.png";
            }

            _context.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public Usuario RemoveUsuario(Guid usuarioId)
        {
            var removeUsuario = _context.Usuarios.Single(x => x.UsuarioId == usuarioId);

            if (removeUsuario == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado.");
            }

            _context.Remove(removeUsuario);
            _context.SaveChanges();
            return removeUsuario;
        }

        public Usuario UpdateUsuario(Guid usuarioId, UsuarioRequest request)
        {
            // Verificar si los campos obligatorios están vacíos o nulos
            if (string.IsNullOrWhiteSpace(request.Nombre))
            {
                throw new InvalidDataException("El campo Nombre no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(request.Apellido))
            {
                throw new InvalidDataException("El campo Apellido no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(request.Username))
            {
                throw new InvalidDataException("El campo Nombre de Usuario no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new InvalidDataException("El campo Correo Electrónico no puede estar vacío.");
            }

            var usuarioToUpdate = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == usuarioId);
            
            if (usuarioToUpdate == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }

            usuarioToUpdate.Nombre = request.Nombre;
            usuarioToUpdate.Apellido = request.Apellido;
            usuarioToUpdate.Username = request.Username;
            usuarioToUpdate.Email = request.Email;
            usuarioToUpdate.FotoPerfil = request.FotoPerfil;


            _context.Update(usuarioToUpdate);
            _context.SaveChanges();


            return usuarioToUpdate;
        }
    }
}