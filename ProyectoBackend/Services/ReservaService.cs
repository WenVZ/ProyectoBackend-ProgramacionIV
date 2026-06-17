using Microsoft.EntityFrameworkCore;
using ProyectoBackend.Data;
using ProyectoBackend.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoBackend.Services
{
    public class ReservaService
    {
        private readonly ApplicationDbContext _context;

        public ReservaService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todas las reservas (Solo para el panel del Admin)
        public async Task<List<Reserva>> ObtenerTodasAsync()
        {
            return await _context.Reservas.ToListAsync();
        }

        public async Task<List<Reserva>> ObtenerPorEmailAsync(string email)
        {
            return await _context.Reservas
                .Where(r => r.Email == email)
                .ToListAsync();
        }

        // Obtener una reserva específica por su ID
        public async Task<Reserva?> ObtenerPorIdAsync(int id)
        {
            return await _context.Reservas.FindAsync(id);
        }

        // Crear una nueva reserva (Público)
        public async Task<Reserva> CrearAsync(Reserva reserva)
        {
            if (string.IsNullOrWhiteSpace(reserva.Estado)) reserva.Estado = "Pendiente";

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
            return reserva;
        }

        // Actualizar la reserva / Cambiar Estado (Solo Admin)
        public async Task<bool> ActualizarAsync(int id, Reserva datosActualizados)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return false;

            reserva.Nombre = datosActualizados.Nombre;
            reserva.Email = datosActualizados.Email;
            reserva.Fecha = datosActualizados.Fecha;
            reserva.HoraEvento = datosActualizados.HoraEvento;
            reserva.Personas = datosActualizados.Personas;
            reserva.Actividad = datosActualizados.Actividad;
            reserva.Estado = datosActualizados.Estado; // "Pendiente", "Aprobada", "Cancelada"

            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar una reserva del historial (Solo Admin)
        public async Task<bool> EliminarAsync(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return false;

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}