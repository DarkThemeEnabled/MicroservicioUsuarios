namespace Domain.DTO
{
    public class UsuarioDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int FotoPerfil { get; set; }
    }
    public class UserDtoSinPassword
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public int FotoPerfil { get; set; }
    }
}
