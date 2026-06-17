using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoBackend.Entities;
using ProyectoBackend.Services;
using System.Threading.Tasks;

namespace ProyectoBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidenciasController : ControllerBase
    {
        private readonly IncidenciaService _incidenciaService;

        public IncidenciasController(IncidenciaService incidenciaService)
        {
            _incidenciaService = incidenciaService;
        }

        // GET: api/incidencias
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _incidenciaService.ObtenerTodasAsync();
            return Ok(lista);
        }

        // GET: api/incidencias/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var incidencia = await _incidenciaService.ObtenerPorIdAsync(id);
            if (incidencia == null) return NotFound(new { mensaje = "Incidencia no encontrada." });
            return Ok(incidencia);
        }

        // POST: api/incidencias
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Incidencia incidencia)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var nuevo = await _incidenciaService.CrearAsync(incidencia);
            return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
        }

        // PUT: api/incidencias/{id}
        //SOLO EL ADMINISTRADOR
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Incidencia incidencia)
        {
            var actualizado = await _incidenciaService.ActualizarAsync(id, incidencia);
            if (!actualizado) return NotFound(new { mensaje = "No se encontró la incidencia para actualizar." });

            return Ok(new { mensaje = "Incidencia actualizada correctamente." });
        }

        // DELETE: api/incidencias/{id}
        //SOLO EL ADMINISTRADOR

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _incidenciaService.EliminarAsync(id);
            if (!eliminado) return NotFound(new { mensaje = "No se encontró la incidencia para eliminar." });

            return Ok(new { mensaje = "Incidencia eliminada con éxito." });
        }
    }
}