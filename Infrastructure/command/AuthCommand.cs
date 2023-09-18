using Application.Interfaces;
using Application.Request;
using Application.Response;
using Application.UseCases;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Command
{
    public class AuthCommand : IAuthCommand
    {
        private readonly UsuarioContext _context;
        private readonly IConfiguration _configuration;
        private readonly IPasswordService _passwordService;

        public AuthCommand(UsuarioContext context, IConfiguration configuration, IPasswordService passwordService)
        {
            _context = context;
            _configuration = configuration;
            _passwordService = passwordService;
        }

        public async Task<AuthResponse> Registrar(RegisterRequest request)
        {
            var usuario = new Usuario
            {
                UsuarioId = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = _passwordService.HashPassword(request.Password)
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var jwtToken = GenerateJwtToken(usuario);

            return new AuthResponse
            {
                Token = jwtToken,
                Mensaje = "Registro exitoso"
            };
        }

        public async Task<AuthResponse> Login(LoginRequest request)
        {
            var usuario = await _context.Usuarios.SingleOrDefaultAsync(x => x.Email == request.Email);

            if (usuario == null || !_passwordService.VerifyPassword(request.Password, usuario.PasswordHash))  // Usa PasswordService para verificar la contraseña
            {
                return new AuthResponse
                {
                    Mensaje = "Email o contraseña incorrecta"
                };
            }

            var jwtToken = GenerateJwtToken(usuario);

            return new AuthResponse
            {
                Token = jwtToken,
                Mensaje = "Inicio de sesión exitoso"  // Puedes configurar el mensaje según sea necesario
            };
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Email, usuario.Email)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JwtConfig:Issuer"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}