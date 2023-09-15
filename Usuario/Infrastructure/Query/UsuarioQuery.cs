namespace Infrastructure.Query
{
    public class UsuarioQuery
    {
        // GetUsuariosQuery
        public class GetUsuariosQuery
        {
            public string Nombre { get; set; }
            public string Email { get; set; }
        }

        // GetUsuarioByIdQuery
        public class GetUsuarioByIdQuery
        {
            public int UsuarioId { get; set; }

            public GetUsuarioByIdQuery(int usuarioId)
            {
                UsuarioId = usuarioId;
            }
        }

    }
}
