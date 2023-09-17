namespace Application.Request
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmed { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
