using System.ComponentModel.DataAnnotations;

namespace ProyectoBackend.Entities
{
  
        public class Evento
        {
            [Key]
            public int Id { get; set; }

            [Required]
            public string Nombre { get; set; } = string.Empty;

            [Required]
            public string Fecha { get; set; } = string.Empty; //hora que se creo para guardar en supabase

            public string Hora { get; set; } = string.Empty;

            [Required]
            public string Descripcion { get; set; } = string.Empty;

            public string Imagen { get; set; } = string.Empty;

            [Required]
            public int Cupos { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
    }

