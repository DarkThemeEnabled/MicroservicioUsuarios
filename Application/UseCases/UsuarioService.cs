using Application.Interfaces;
using Application.Request;
using Application.Response;
using Domain.Entities;
using Application.Exceptions;
using Application.Helpers;

namespace Application.UseCase.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioCommand _command;
        private readonly IUsuarioQuery _query;
        private readonly ITokenService _tokenService;

        public UsuarioService(IUsuarioCommand command, IUsuarioQuery query, ITokenService tokenService)
        {
            _command = command;
            _query = query;
            _tokenService = tokenService;
        }

        public UsuarioTokenResponse Authenticacion(UsuarioLoginRequest request)
        {
            UsuarioResponse userLogged = new UsuarioResponse();

            string password = Encrypt.GetSHA256(request.Password);
            var usuarioEncontrado = _query.UserLogin(request.Email, password);

            if (usuarioEncontrado == null) return null;

            return _tokenService.GenerateToken(usuarioEncontrado);
        }

        public UsuarioResponse CreateUsuario(UsuarioPasswordRequest request)
        {
            string caracteresEspeciales = "!\"·$%&/()=¿¡?'_:;,|@#€*+.";
            bool existenCaracteresEspeciales = (caracteresEspeciales.Intersect(request.Password).Count() > 0);

            if (!existenCaracteresEspeciales)
            {
                throw new PasswordFormatException("La password requiere al menos un caracter especial");
            }

            if (request.Password.Length < 8)
            {
                throw new PasswordFormatException("La password requiere al menos 8 caracteres.");
            }

            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Username = request.Username,
                Email = request.Email,
                FotoPerfil = request.FotoPerfil,
                Password = Encrypt.GetSHA256(request.Password)
            };

            _command.CreateUsuario(usuario);

            return new UsuarioResponse
            {
                UsuarioId = usuario.UsuarioId,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Username = usuario.Username, 
                Email = usuario.Email, 
                FotoPerfil = usuario.FotoPerfil
            };
        }

        public UsuarioResponse GetUsuarioById(Guid usuarioId)
        {
            var usuario = _query.GetUsuarioById(usuarioId);

            if (usuario != null)
            {
                return new UsuarioResponse
                {
                    UsuarioId = usuario.UsuarioId,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Username = usuario.Username,
                    Email = usuario.Email,
                    FotoPerfil = usuario.FotoPerfil
                };
            }
            return null;
        }

        public List<Usuario> GetUsuarioList()
        {
            return _query.GetUsuarioList();
        }

        public UsuarioDeleteResponse RemoveUsuario(Guid usuarioId)
        {
            var usuario = _command.RemoveUsuario(usuarioId);

            return new UsuarioDeleteResponse
            {
                UsuarioId = usuario.UsuarioId,
                Username = usuario.Username,
                Email = usuario.Email,
            };
        }

        public UsuarioResponse UpdateUsuario(Guid usuarioId, UsuarioRequest request)
        {
            var usuario = _command.UpdateUsuario(usuarioId, request);

            return new UsuarioResponse
            {
                UsuarioId = usuario.UsuarioId,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Username = usuario.Username,
                Email = usuario.Email,
                FotoPerfil = usuario.FotoPerfil
            };
        }
    }
}