using Microsoft.EntityFrameworkCore;
using ProyectoBackend.Data;
using ProyectoBackend.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoBackend.Services
{
    public class IncidenciaService
    {
        private readonly ApplicationDbContext _context;

        public IncidenciaService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todas las incidencias
        public async Task<List<Incidencia>> ObtenerTodasAsync()
        {
            return await _context.Incidencias.ToListAsync();
        }

        // Obtener una incidencia por ID
        public async Task<Incidencia?> ObtenerPorIdAsync(int id)
        {
            return await _context.Incidencias.FindAsync(id);
        }

        // Crear reporte 
        public async Task<Incidencia> CrearAsync(Incidencia incidencia)
        {
            if (string.IsNullOrWhiteSpace(incidencia.Nombre)) incidencia.Nombre = "Anónimo";
            if (string.IsNullOrWhiteSpace(incidencia.Estado)) incidencia.Estado = "Pendiente";
            if (string.IsNullOrWhiteSpace(incidencia.Prioridad)) incidencia.Prioridad = "Normal";

            _context.Incidencias.Add(incidencia);
            await _context.SaveChangesAsync();
            return incidencia;
        }

        // Actualizar Estado/Prioridad 
        public async Task<bool> ActualizarAsync(int id, Incidencia datosActualizados)
        {
            var incidencia = await _context.Incidencias.FindAsync(id);
            if (incidencia == null) return false;

            incidencia.Nombre = datosActualizados.Nombre;
            incidencia.Tipo = datosActualizados.Tipo;
            incidencia.Ubicacion = datosActualizados.Ubicacion;
            incidencia.Descripcion = datosActualizados.Descripcion;
            incidencia.Imagen = datosActualizados.Imagen;
            incidencia.Estado = datosActualizados.Estado;       
            incidencia.Prioridad = datosActualizados.Prioridad;   
            incidencia.Fecha = datosActualizados.Fecha;

            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar incidencia
        //mas adelante solo el admin
        public async Task<bool> EliminarAsync(int id)
        {
            var incidencia = await _context.Incidencias.FindAsync(id);
            if (incidencia == null) return false;

            _context.Incidencias.Remove(incidencia);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}