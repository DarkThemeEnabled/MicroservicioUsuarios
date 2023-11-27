using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Application.Response;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Application.UseCases
{
    public class TokenService : ITokenService
    {
        private readonly string _claveFirma;

        public TokenService(string claveFirma)
        {
            _claveFirma = claveFirma;
        }

        public UsuarioTokenResponse GenerateToken(Usuario userLogin)
        {

            var header = new JwtHeader(
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_claveFirma)
                    ),
                    SecurityAlgorithms.HmacSha256)
            );

            var claims = new Claim[]
            {
            new Claim(JwtRegisteredClaimNames.UniqueName, userLogin.UsuarioId.ToString()),
            new Claim(JwtRegisteredClaimNames.Name , userLogin.Nombre),
            new Claim("Apellido", userLogin.Apellido),
            new Claim(JwtRegisteredClaimNames.Email, userLogin.Email),
            };

            var payload = new JwtPayload(claims) {
            // Agregar una fecha de expiración de 1 hora desde ahora
                {"exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds()},
                {"aud", "usuarios" },
                {"iss", "localhost" }
            };

            var token = new JwtSecurityToken(header, payload);
            var tokenUsuairo = new JwtSecurityTokenHandler().WriteToken(token);

            return new UsuarioTokenResponse
            {
                UsuarioId = userLogin.UsuarioId,
                Token = tokenUsuairo
            };

        }
        public bool IsTokenExpired(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            return jwtToken.ValidTo < DateTime.UtcNow;
        }
    }
}