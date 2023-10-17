using System.ComponentModel.DataAnnotations;

namespace Application.Request
{
    public class UsuarioLoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
