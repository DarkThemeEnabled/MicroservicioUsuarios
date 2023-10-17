using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Exceptions;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Authorization;
using BadRequest = Application.Response.BadRequest;
using Microsoft.AspNetCore.Http.HttpResults;
using ConflictException = Application.Exceptions.ConflictException;
using NotFound = Application.Response.NotFound;
using Conflict = Application.Response.Conflict;
using Application.UseCases;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;

        public UsuarioController(IUsuarioService usuarioService, ITokenService tokenService)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(UsuarioTokenResponse), 200)]
        public IActionResult LoginAuth(UsuarioLoginRequest userLogin)
        {
            var userResponse = _usuarioService.Authenticacion(userLogin);

            if (userResponse == null) { return BadRequest(new BadRequest { Message = "Usuario invalido" }); };

            return Ok(userResponse);
        }


        /// <summary>
        /// devuelve un usuario
        /// </summary>
        //[Authorize]
        [HttpGet("{usuarioId}")]
        [ProducesResponseType(typeof(UsuarioResponse), 200)]
        public IActionResult GetUsuarioById(string usuarioId)
        {
            Guid usuarioIdBuscar = new Guid();

            if (!Guid.TryParse(usuarioId, out usuarioIdBuscar))
            {
                return new JsonResult(new BadRequest { Message = "El formato del id no es valido." });
            }

            var result = _usuarioService.GetUsuarioById(usuarioIdBuscar);

            if (result == null)
            {
                return NotFound(new BadRequest { Message = "No se encontraró el usuario con ese id." });
            }

            return new JsonResult(result);
        }

        /// <summary>
        /// crea un usuario nuevo
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType(typeof(UsuarioResponse), 201)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        public IActionResult CreateUsuario(UsuarioPasswordRequest request)
        {
            UsuarioResponse result = null;

            try
            {
                result = _usuarioService.CreateUsuario(request);
            }
            catch (ExistingMailException e)
            {
                return new JsonResult(new BadRequest { Message = e.Message }) { StatusCode = 409 };
            }
            catch (PasswordFormatException e)
            {
                return new JsonResult(new BadRequest { Message = e.Message }) { StatusCode = 409 };
            }
            catch (Exception)
            {
                return new JsonResult(new BadRequest { Message = "Puede que existan campos invalidos" }) { StatusCode = 400 };
            }

            return new JsonResult(result);
        }

        /// <summary>
        /// modifica un usuario existente
        /// </summary>
        [Authorize]
        [HttpPut("{usuarioId}")]
        [ProducesResponseType(typeof(UsuarioResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(NotFound), 404)]
        public IActionResult UpdateUsuario(Guid usuarioId, UsuarioRequest request)
        {
            // Obtener el token desde el encabezado de la solicitud
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Verificar si el token ha caducado
            if (_tokenService.IsTokenExpired(token))
            {
                return Unauthorized("El token ha caducado.");
            }

            // Resto de la lógica de tu método...
            UsuarioResponse result = null;

            try
            {
                result = _usuarioService.UpdateUsuario(usuarioId, request);
            }
            catch (InvalidDataException ex)
            {
                return BadRequest(new BadRequest { Message = ex.Message });
            }

            return new JsonResult(result) { StatusCode = 200 };
        }


        /// <summary>
        /// elimina un usuario existente
        /// </summary>
        [Authorize]
        [HttpDelete("{usuarioId}")]
        [ProducesResponseType(typeof(UsuarioDeleteResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(NotFound), 404)]
        [ProducesResponseType(typeof(Conflict), 409)]
        public IActionResult DeleteUsuario(Guid usuarioId)
        {
            try
            {
                var result = _usuarioService.RemoveUsuario(usuarioId);
                return Ok(result);
            }
            catch (UserNotFoundException)
            {
                // Considera registrar el error (e.Message) aquí para fines de depuración.
                return BadRequest(new BadRequest { Message = "Hubo un problema al eliminar el usuario." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new NotFound { Message = "Ese usuario no existe" });
            }
            catch (ConflictException ex)
            {
                return StatusCode(409, new Conflict { Message = ex.Message });
            }
        }
    }
}