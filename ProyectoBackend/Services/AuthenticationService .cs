using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyectoBackend.Data;
using ProyectoBackend.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace ProyectoBackend.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AuthenticationService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<string?> LoginAsync(string correo, string password)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo);
            if (usuario == null) return null;

            // compara el password ingresado contra el hash guardado 
            bool passwordValida = BC.Verify(password, usuario.PasswordHash);
            if (!passwordValida) return null;

            return GenerarToken(usuario.Correo, usuario.Rol);
        }

        public async Task<bool> RegisterAsync(string nombre, string correo, string password)
        {
            var existe = await _context.Usuarios.AnyAsync(u => u.Correo == correo);
            if (existe) return false;

            var usuario = new Usuario
            {
                Nombre = nombre,
                Correo = correo,
                PasswordHash = BC.HashPassword(password), // no se guarda el password en texto plano
                Rol = "usuario",
                FechaRegistro = DateTime.UtcNow
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        private string GenerarToken(string correo, string rol)
        {
            // Claims: info que va dentro del token, para identificar al usuario sin consultar la BD
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, correo),
                new Claim(ClaimTypes.Role, rol),
            };

            // Clave secreta usada para firmar el token 
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8), // tiempo de vida del token
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token); // serializa el token a string
        }
    }
}