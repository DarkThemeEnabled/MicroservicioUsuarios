namespace Application.Response
{
    public class UsuarioResponse
    {
        public Guid UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FotoPerfil { get; set; }
    }
}
