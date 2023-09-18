using Application.Interfaces;
using Application.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IAuthCommand _authCommand;
        //private readonly IComentarioApi _comentarioApi;
        //private readonly IRecetaApi _recetaApi;

        public UsuarioController(IUsuarioService usuarioService, IAuthCommand authCommand/*, IComentarioApi comentarioApi, IRecetaApi recetaApi*/)
        {
            _usuarioService = usuarioService;
            _authCommand = authCommand;
            //_comentarioApi = comentarioApi;
            //_recetaApi = recetaApi;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosUsuarios()
        {
            try
            {
                var usuarios = await _usuarioService.ObtenerTodosLosUsuarios();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuarioPorId(Guid id)
        {
            try
            {
                var usuario = await _usuarioService.ObtenerUsuarioPorId(id);
                if (usuario == null)
                {
                    return NotFound(new { message = "Usuario no encontrado" });
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("Registro")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioCrearRequest request)
        {
            try
            {
                var usuario = await _usuarioService.RegistrarUsuario(request.Nombre, request.Apellido, request.Email, request.Username, request.FotoPerfil, request.Password);
                return CreatedAtAction(nameof(ObtenerUsuarioPorId), new { id = usuario.UsuarioId }, usuario);
                // Verificar si se proporcionó una foto de perfil
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(Guid id, [FromBody] UsuarioActualizarRequest request)
        {
            try
            {
                var usuario = await _usuarioService.ActualizarUsuario(id, request.Nombre, request.Apellido, request.Email, request.FotoPerfil, request.Password);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(Guid id)
        {
            try
            {
                await _usuarioService.EliminarUsuario(id);
                return Ok(new { message = "Usuario eliminado con éxito" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }
            catch (UnauthorizedAccessException)
            {
                var response = new ContentResult
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Content = "No tienes permiso para eliminar esta cuenta",
                    ContentType = "text/plain"
                };
                return response;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                // Autenticar al usuario utilizando el servicio AuthCommand
                var authResponse = await _authCommand.Login(request);

                if (authResponse == null)
                {
                    return BadRequest(new { message = "Email o contraseña incorrectos" });
                }

                return Ok(new { Token = authResponse.Token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }

    public class UsuarioCrearRequest
    {
        [Required]
        public required string Nombre { get; set; }
        [Required]
        public required string Apellido { get; set; }
        public required string FotoPerfil { get; set; }
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }

    public class UsuarioActualizarRequest
    {

        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string FotoPerfil { get; set; }
        public required string Password { get; set; }
    }
}
