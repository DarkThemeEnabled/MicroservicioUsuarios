using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        //private readonly IComentarioApi _comentarioApi;
        //private readonly IRecetaApi _recetaApi;

        public UsuarioController(IUsuarioService usuarioService/*, IComentarioApi comentarioApi, IRecetaApi recetaApi*/)
        {
            _usuarioService = usuarioService;
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

        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioCrearRequest request)
        {
            try
            {
                var usuario = await _usuarioService.RegistrarUsuario(request.Nombre, request.Apellido, request.Email, request.FotoPerfil);
                return CreatedAtAction(nameof(ObtenerUsuarioPorId), new { id = usuario.UsuarioId }, usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(Guid id, [FromBody] UsuarioActualizarRequest request)
        {
            try
            {
                var usuario = await _usuarioService.ActualizarUsuario(id, request.Nombre, request.Apellido, request.Email, request.FotoPerfil);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(Guid id)
        {
            try
            {
                await _usuarioService.EliminarUsuario(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    public class UsuarioCrearRequest
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string FotoPerfil { get; set; }
    }

    public class UsuarioActualizarRequest
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string FotoPerfil { get; set; }
    }
}
