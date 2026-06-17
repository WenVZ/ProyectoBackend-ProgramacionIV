using Microsoft.EntityFrameworkCore;
using ProyectoBackend.Data;
using ProyectoBackend.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoBackend.Services
{
    public class EventoService
    {
        private readonly ApplicationDbContext _context;

        public EventoService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todos los eventos activos en este momento

        public async Task<List<Evento>> ObtenerTodosAsync()
        {
            return await _context.Eventos.ToListAsync();
        }

        // Obtener un evento específico por ID
        public async Task<Evento?> ObtenerPorIdAsync(int id)
        {
            return await _context.Eventos.FindAsync(id);
        }



        // Crear un nuevo evento (Solo Admin)
        public async Task<Evento> CrearAsync(Evento evento)
        {
            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();
            return evento;
        }



        // Actualizar datos de un evento (Solo Admin puede)
        public async Task<bool> ActualizarAsync(int id, Evento datosActualizados)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null) return false;

            evento.Nombre = datosActualizados.Nombre;
            evento.Fecha = datosActualizados.Fecha;
            evento.Hora = datosActualizados.Hora;
            evento.Descripcion = datosActualizados.Descripcion;
            evento.Imagen = datosActualizados.Imagen;
            evento.Cupos = datosActualizados.Cupos;

            await _context.SaveChangesAsync();
            return true;
        }



        // Eliminar un evento (Solo Admin)
        public async Task<bool> EliminarAsync(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null) return false;

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}