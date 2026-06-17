using Microsoft.EntityFrameworkCore;
using ProyectoBackend.Entities;
using System.Collections.Generic;

namespace ProyectoBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Emprendimiento> Emprendimientos { get; set; }
        public DbSet<Incidencia> Incidencias { get; set; }
        public DbSet<Evento> Eventos { get; set; }
    }

}