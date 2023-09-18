using Application.Interfaces;
using Application.Request;
using Application.UseCases;
using Domain.DTO;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthCommand _command;
        private readonly IAuthQuery _query;
        private readonly string _jwtKey;
        private readonly string _jwtIssuer;
        private readonly PasswordService _passwordService;

        public AuthService(IAuthCommand command, IAuthQuery query, string jwtKey, string jwtIssuer, PasswordService passwordService)
        {
            _command = command;
            _query = query;
            _jwtKey = jwtKey;
            _jwtIssuer = jwtIssuer;
            _passwordService = passwordService;
        }

        public async Task<bool> VerifyCredentials(string email, string password)
        {
            // Realiza una consulta para obtener el usuario por su correo electrónico
            var user = await _query.GetUsuarioByEmail(email);

            // Si el usuario no existe, o las contraseñas no coinciden, retorna false
            if (user == null || !_passwordService.VerifyPassword(password, user.PasswordHash))
            {
                return false;
            }

            return true; // Las credenciales son válidas
        }

        public async Task<string> Register(string email, string password)
        {
            // Implementación del registro (ej. guardar el usuario en la base de datos)
            await _command.Registrar(new RegisterRequest { Email = email, Password = _passwordService.HashPassword(password) });
            return GenerateJwtToken(email);
        }

        public async Task<string?> Login(string email, string password)
        {
            var user = await _query.GetUsuarioByEmail(email);

            if (user == null)
            {
                return null;
            }

            if (!_passwordService.VerifyPassword(password, user.PasswordHash))
            {
                return null;
            }

            string token = GenerateJwtToken(email);
            return token;
        }

        private string GenerateJwtToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddHours(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtIssuer,
                Audience = _jwtIssuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<UsuarioDTO>? ObtenerUsuarioPorToken(string token)
        {
            var key = Encoding.UTF8.GetBytes(_jwtKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtIssuer,
                ValidAudience = _jwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                if (claimsPrincipal.Identity.IsAuthenticated)
                {
                    var usuario = new UsuarioDTO
                    {
                        Email = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value,
                        // Otras propiedades del usuario que puedas necesitar
                    };

                    return usuario;
                }
            }
            catch (Exception ex)
            {
                // Manejar errores de decodificación de token aquí
                // Puedes registrar el error, lanzar una excepción personalizada, etc.
                Console.WriteLine("Error al decodificar el token: " + ex.Message);
            }

            return null;
        }

        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
            return await _query.GetUsuarioByEmail(email);
        }
        public bool IsPasswordValid(string password)
        {
            // Longitud mínima (por ejemplo, 8 caracteres)
            int minLength = 8;
            return password.Length >= minLength;
        }
        public bool IsPasswordComplex(string password)
        {
            // Utiliza una expresión regular para verificar la complejidad de la contraseña
            string pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%^&+=!]).{8,}$";
            return Regex.IsMatch(password, pattern);
        }

    }
}