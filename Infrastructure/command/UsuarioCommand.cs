using Domain.DTO;

namespace Infrastructure.Command
{
    public class UsuarioCommand
    {
        // CreateUsuarioCommand
        public class CreateUsuarioCommand
        {
            public UsuarioDto Usuario { get; set; }

            public CreateUsuarioCommand(UsuarioDto usuario)
            {
                Usuario = usuario;
            }
        }

        // DeleteUsuarioCommand
        public class DeleteUsuarioCommand
        {
            public int UsuarioId { get; set; }

            public DeleteUsuarioCommand(int usuarioId)
            {
                UsuarioId = usuarioId;
            }
        }

        // UpdateUsuarioCommand
        public class UpdateUsuarioCommand
        {
            public int UsuarioId { get; set; }
            public UsuarioDto Usuario { get; set; }

            public UpdateUsuarioCommand(int usuarioId, UsuarioDto usuario)
            {
                UsuarioId = usuarioId;
                Usuario = usuario;
            }
        }

        // RegisterUsuarioCommand
        public class RegisterUsuarioCommand
        {
            public UsuarioDto Usuario { get; set; }

            public RegisterUsuarioCommand(UsuarioDto usuario)
            {
                Usuario = usuario;
            }
        }

        // AuthenticateUsuarioCommand
        public class AuthenticateUsuarioCommand
        {
            public string Email { get; set; }
            public string Password { get; set; }

            public AuthenticateUsuarioCommand(string email, string password)
            {
                Email = email;
                Password = password;
            }
        }
    }
}
