using Application.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using BCrypt.Net;

namespace Infrastructure.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioQuery _query;
        private readonly IUsuarioCommand _command;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioQuery query, IUsuarioRepository usuarioRepository, IUsuarioCommand command)
        {
            _query = query;
            _usuarioRepository = usuarioRepository;
            _command = command;
        }

        public Task<Usuario> Register(string nombre, string apellido, string username, string fotoPerfil, string email, string password)
        {
            return _command.Register(nombre, apellido, username, fotoPerfil, email, password);
        }

        public Task<Usuario> UpdateUsuario(Guid usuarioId, string nombre, string apellido, string email, string fotoPerfil, string password)
        {
            return _command.UpdateUsuario(usuarioId, nombre, apellido, email, fotoPerfil, password);
        }

        public Task DeleteUsuario(Guid usuarioId)
        {
            return _command.DeleteUsuario(usuarioId);
        }

        public Task<IEnumerable<Usuario>> GetAll()
        {
            return _query.GetAll();
        }

        public Task<Usuario> GetById(Guid usuarioId)
        {
            return _query.GetById(usuarioId);
        }

        public Task<IEnumerable<Usuario>> GetByName(string nombre)
        {
            return _query.GetByName(nombre);
        }

        public async Task<UsuarioDTO> ValidateUserCredentials(UsuarioLoginDTO usuarioLoginDto)
        {
            var usuario = await _usuarioRepository.GetByEmail(usuarioLoginDto.Email);

            if (usuario == null || !CheckPassword(usuarioLoginDto.Password, usuario.PasswordHash))
            {
                return null;
            }

            return new UsuarioDTO
            {
                // Map properties from Usuario to UsuarioDTO
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Username = usuario.Username,
                Email = usuario.Email,
                FotoPerfil = usuario.FotoPerfil
            };
        }

        private bool CheckPassword(string inputPassword, string storedHash)
        {
            try
            {
                // Verifica la contraseña ingresada contra el hash almacenado
                return BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
            }
            catch
            {
                // Log or handle error (like logging) as per your need
                return false;
            }
        }
    }
}