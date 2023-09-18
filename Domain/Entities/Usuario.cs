namespace Domain.Entities
{
    public class Usuario
    {
        public Guid UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? FotoPerfil { get; set; }
        public string PasswordHash { get; set; }
    }
}
