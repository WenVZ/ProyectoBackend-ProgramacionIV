using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoBackend.Entities;
using ProyectoBackend.Services;
using System.Threading.Tasks;

namespace ProyectoBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmprendimientosController : ControllerBase
    {
        private readonly EmprendimientoService _emprendimientoService;

        public EmprendimientosController(EmprendimientoService emprendimientoService)
        {
            _emprendimientoService = emprendimientoService;
        }

        // GET: api/emprendimientos (Cualquiera puede verlos)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _emprendimientoService.ObtenerTodosAsync();
            return Ok(lista);
        }

        // GET: api/emprendimientos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var emprendimiento = await _emprendimientoService.ObtenerPorIdAsync(id);
            if (emprendimiento == null) return NotFound(new { mensaje = "Emprendimiento no encontrado" });
            return Ok(emprendimiento);
        }

        // POST: api/emprendimientos (SOLO ADMIN)
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] Emprendimiento emprendimiento)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var nuevo = await _emprendimientoService.CrearAsync(emprendimiento);
            return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
        }

        // PUT: api/emprendimientos/{id} (SOLO ADMIN)
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Emprendimiento emprendimiento)
        {
            var actualizado = await _emprendimientoService.ActualizarAsync(id, emprendimiento);
            if (!actualizado) return NotFound(new { mensaje = "Emprendimiento no encontrado para actualizar" });

            return Ok(new { mensaje = "Emprendimiento actualizado con éxito" });
        }

        // DELETE: api/emprendimientos/{id} (SOLO ADMIN)
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _emprendimientoService.EliminarAsync(id);
            if (!eliminado) return NotFound(new { mensaje = "Emprendimiento no encontrado" });

            return Ok(new { mensaje = "Emprendimiento eliminado correctamente" });
        }
    }
}