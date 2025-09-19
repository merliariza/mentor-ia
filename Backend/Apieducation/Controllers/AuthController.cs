using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs.Auth;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

namespace ApiPortfolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly PublicDbContext _context;

        public AuthController(IConfiguration config, PublicDbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificar si ya existe el correo
            var exists = await _context.UserMembers.AnyAsync(u => u.Email == request.Email);
            if (exists)
            {
                return BadRequest(new { Message = "El usuario ya existe." });
            }

            // Crear nuevo usuario
            var user = new UserMember
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _context.UserMembers.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Usuario registrado exitosamente." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _context.UserMembers
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return Unauthorized(new DataUserDto
                {
                    Codeb = "401",
                    Message = "El usuario no existe.",
                    IsAuthenticated = false
                });
            }

            // Verificar la contraseña
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized(new DataUserDto
                {
                    Codeb = "401",
                    Message = "Contraseña incorrecta.",
                    IsAuthenticated = false
                });
            }

            // Generar token
            var token = GenerateJwtToken(user);

            var response = new DataUserDto
            {
                Codeb = "200",
                Message = "Autenticación exitosa.",
                IsAuthenticated = true,
                Email = user.Email,
                Name = user.FullName,
                UserName = user.Email,
                Token = token,
                RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(30)
            };

            return Ok(response);
        }

        private string GenerateJwtToken(UserMember user)
        {
            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("email", user.Email ?? ""),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var durationInMinutes = int.Parse(_config["Jwt:DurationInMinutes"]);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(durationInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
