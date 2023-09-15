namespace Domain.DTO
{
    public class UserByIdDto
    {
        public int UsuarioId { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public int FotoPerfil { get; set; }

    }
}
