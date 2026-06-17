using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoBackend.Entities;
using ProyectoBackend.Services;
using System.Threading.Tasks;

namespace ProyectoBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly EventoService _eventoService;

        public EventosController(EventoService eventoService)
        {
            _eventoService = eventoService;
        }

        // GET: api/eventos (Público)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _eventoService.ObtenerTodosAsync();
            return Ok(lista);
        }

        // GET: api/eventos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var evento = await _eventoService.ObtenerPorIdAsync(id);
            if (evento == null) return NotFound(new { mensaje = "Evento no encontrado" });
            return Ok(evento);
        }

        // POST: api/eventos (SOLO ADMINISTRADOR)
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] Evento evento)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var nuevo = await _eventoService.CrearAsync(evento);
            return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
        }
        // PUT: api/eventos/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] Evento evento)
        {
            var rol = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

          
            var actualizado = await _eventoService.ActualizarAsync(id, evento);
            if (!actualizado) return NotFound(new { mensaje = "Evento no encontrado" });
            return Ok(new { mensaje = "Evento actualizado con éxito" });
        }

        // DELETE: api/eventos/{id} (SOLO ADMIN)
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _eventoService.EliminarAsync(id);
            if (!eliminado) return NotFound(new { mensaje = "Evento no encontrado" });

            return Ok(new { mensaje = "Evento eliminado correctamente" });
        }
    }
}