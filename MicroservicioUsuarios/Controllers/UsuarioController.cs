using Application.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domain.DTO;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http.HttpResults;
using Infrastructure.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly string _myToken;
        private readonly IUsuarioService _usuarioService;
        private readonly HttpClient _httpClient;

        public UsuarioController(IConfiguration configuration, IUsuarioService usuarioService, IHttpClientFactory httpClientFactory)
        {
            // Acceder al secret desde User Secrets
            _myToken = configuration["Token"];
            _usuarioService = usuarioService;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegistered()
        {
            var usuarios = await _usuarioService.GetAllRegistered();
            return Ok(usuarios);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(UsuarioLogueadoDTO), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 409)]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO usuarioLoginDto)
        {
            try
            {
                var user = await _usuarioService.ValidateUserCredentials(usuarioLoginDto);

                if (user != null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _myToken);

                    try
                    {
                        var response = await _httpClient.GetAsync("https://localhost:7015/api/");

                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            // Procesar `data` si es necesario...
                        }
                    }
                    catch (Exception ex)
                    {
                        // Loggear o manejar la excepción según sea necesario...
                    }

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.Nombre)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties();

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    var usuarioLogueado = new UsuarioLogueadoDTO
                    {
                        Nombre = user.Nombre,
                        Apellido = user.Apellido,
                        Email = user.Email,
                        Username = user.Username,
                        FotoPerfil = user.FotoPerfil
                    };

                    return Ok(usuarioLogueado);
                }

                return Unauthorized(new { Message = "Invalid username or password" });
            }
            catch (Exception ex)
            {
                // Asegúrate de loggear la excepción...
                return StatusCode(500, new { Message = "An error occurred while processing your request." });
            }
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

// Utilizar _mySecretToken para hacer una solicitud HTTP a otra API

//_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _myToken);

//var response = await _httpClient.GetAsync("https://api.example.com/data");

//if (response.IsSuccessStatusCode)
//{
//    var data = await response.Content.ReadAsStringAsync();
//    // Utilizar `data` según sea necesario...
//}