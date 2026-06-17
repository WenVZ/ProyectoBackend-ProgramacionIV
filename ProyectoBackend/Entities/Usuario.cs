using System.ComponentModel.DataAnnotations;

namespace ProyectoBackend.Entities
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string Rol { get; set; } = string.Empty; // "admin" o "usuario"

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}
