using Application.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Query
{
    public class AuthQuery : IAuthQuery
    {
        private readonly UsuarioContext _context;

        public AuthQuery(UsuarioContext context)
        {
            _context = context;
        }

        public async Task<UsuarioDTO> ObtenerUsuarioPorToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                throw new ArgumentException("Token inválido.");

            var emailClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);

            if (emailClaim == null)
                throw new ArgumentException("Token no contiene claim de email.");

            var email = emailClaim.Value;

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
                throw new InvalidOperationException("Usuario no encontrado.");

            return new UsuarioDTO
            {
                Email = usuario.Email,
                Nombre = usuario.Nombre
            };
        }

        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null)
                throw new InvalidOperationException("Usuario no encontrado.");

            return usuario;
        }
    }
}