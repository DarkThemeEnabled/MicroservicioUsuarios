using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioCommand _command;
        private readonly IUsuarioQuery _query;

        public UsuarioService(IUsuarioCommand command, IUsuarioQuery query)
        {
            _command = command;
            _query = query;
        }

        // Implementaciones de IUsuarioCommand
        public Task<Usuario> RegistrarUsuario(string nombre, string apellido, string email, string fotoPerfil)
        {
            return _command.RegistrarUsuario(nombre, apellido, email, fotoPerfil);
        }

        public Task<Usuario> ActualizarUsuario(Guid usuarioId, string nombre, string apellido, string email, string fotoPerfil)
        {
            return _command.ActualizarUsuario(usuarioId, nombre, apellido, email, fotoPerfil);
        }

        public Task EliminarUsuario(Guid usuarioId)
        {
            return _command.EliminarUsuario(usuarioId);
        }

        // Implementaciones de IUsuarioQuery
        public Task<IEnumerable<Usuario>> ObtenerTodosLosUsuarios()
        {
            return _query.ObtenerTodosLosUsuarios();
        }

        public Task<Usuario?> ObtenerUsuarioPorId(Guid usuarioId)
        {
            return _query.ObtenerUsuarioPorId(usuarioId);
        }

        public Task<IEnumerable<Usuario>> ObtenerUsuariosPorNombre(string nombre)
        {
            return _query.ObtenerUsuariosPorNombre(nombre);
        }
    }
}