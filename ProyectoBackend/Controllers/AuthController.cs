using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoBackend.DTO;
using ProyectoBackend.Entities;
using ProyectoBackend.Services;

namespace ProyectoBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : 
        ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }


        //inicio de sesion
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO dto)
        {
            var token = await _authService.LoginAsync(dto.Correo, dto.Password);

            if (token == null)
                return Unauthorized("Credenciales inválidas");

            return Ok(new { token });
        }


        //registro
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var ok = await _authService.RegisterAsync(dto.Nombre, dto.Correo, dto.Password);
            if (!ok) return BadRequest("Este correo ya está registrado.");
            return Ok("Usuario registrado correctamente.");
        }
    }
}