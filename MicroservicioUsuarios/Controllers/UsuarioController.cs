using Application.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domain.DTO;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        // Inyección de dependencias a través del constructor
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO usuarioLoginDto)
        {
            // Valida las credenciales del usuario utilizando el servicio inyectado
            var user = await _usuarioService.ValidateUserCredentials(usuarioLoginDto);

            // Si es válido, crea los claims y la cookie de autenticación
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Nombre)
                    //new Claim(ClaimTypes.Role, user.Rol)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return Ok(); // O redirige según tu flujo de aplicación
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioRegisterDTO usuarioRegisterDto)
        {
            try
            {
                // Crear el usuario
                var usuario = await _usuarioService.Register(
                    usuarioRegisterDto.Nombre,
                    usuarioRegisterDto.Apellido,
                    usuarioRegisterDto.Email,
                    usuarioRegisterDto.Username,
                    usuarioRegisterDto.FotoPerfil,
                    usuarioRegisterDto.Password);

                // Si el usuario se crea con éxito, retorna un 201 Created
                return CreatedAtAction(nameof(Register), new { id = usuario.UsuarioId }, usuario);
            }
            catch (Exception ex)
            {
                // Si hay un error (por ejemplo, el email ya está en uso), retorna un 400 Bad Request
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
