using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoBackend.Entities;
using ProyectoBackend.Services;
using System.Threading.Tasks;

namespace ProyectoBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly ReservaService _reservaService;

        public ReservasController(ReservaService reservaService)
        {
            _reservaService = reservaService;
        }

   
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var rol = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

            if (rol == "admin")
            {
                var todas = await _reservaService.ObtenerTodasAsync();
                return Ok(todas);
            }

            var mias = await _reservaService.ObtenerPorEmailAsync(email!);
            return Ok(mias);
        }

        // GET: api/reservas/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var reserva = await _reservaService.ObtenerPorIdAsync(id);
            if (reserva == null) return NotFound(new { mensaje = "Reserva no encontrada." });
            return Ok(reserva);
        }

        // POST: api/reservas
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Reserva reserva)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var nuevo = await _reservaService.CrearAsync(reserva);
            return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
        }

        // PUT: api/reservas/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Reserva reserva)
        {
            var actualizado = await _reservaService.ActualizarAsync(id, reserva);
            if (!actualizado) return NotFound(new { mensaje = "No se encontró la reserva para actualizar." });

            return Ok(new { mensaje = "Estado de la reserva actualizado correctamente." });
        }

        // DELETE: api/reservas/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _reservaService.EliminarAsync(id);
            if (!eliminado) return NotFound(new { mensaje = "No se encontró la reserva para eliminar." });

            return Ok(new { mensaje = "Reserva eliminada con éxito." });
        }
    }
}