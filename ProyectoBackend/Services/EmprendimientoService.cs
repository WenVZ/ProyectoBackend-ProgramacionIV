using ProyectoBackend.Data;
using ProyectoBackend.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ProyectoBackend.Services
{
    public class EmprendimientoService
    {
        private readonly ApplicationDbContext _context;

        public EmprendimientoService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todos los emprendimientos (publico)
        public async Task<List<Emprendimiento>> ObtenerTodosAsync()
        {
            return await _context.Emprendimientos.ToListAsync();
        }

        // Obtener un emprendimiento por ID
        public async Task<Emprendimiento?> ObtenerPorIdAsync(int id)
        {
            return await _context.Emprendimientos.FindAsync(id);
        }

        // Crear un nuevo emprendimiento (Solo Admin)
        public async Task<Emprendimiento> CrearAsync(Emprendimiento emprendimiento)
        {
            _context.Emprendimientos.Add(emprendimiento);
            await _context.SaveChangesAsync();
            return emprendimiento;
        }

        // Actualizar un emprendimiento (Solo Admin)
        public async Task<bool> ActualizarAsync(int id, Emprendimiento datosActualizados)
        {
            var emprendimiento = await _context.Emprendimientos.FindAsync(id);
            if (emprendimiento == null) return false;

            emprendimiento.Nombre = datosActualizados.Nombre;
            emprendimiento.Descripcion = datosActualizados.Descripcion;
            emprendimiento.Categoria = datosActualizados.Categoria;

            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar un emprendimiento (Solo Admin)
        public async Task<bool> EliminarAsync(int id)
        {
            var emprendimiento = await _context.Emprendimientos.FindAsync(id);
            if (emprendimiento == null) return false;

            _context.Emprendimientos.Remove(emprendimiento);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
