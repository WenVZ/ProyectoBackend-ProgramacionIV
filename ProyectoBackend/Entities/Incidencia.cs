namespace ProyectoBackend.Entities
{
    public class Incidencia
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = "Anónimo";

        public string Tipo { get; set; } = string.Empty;

        public string Ubicacion { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public string Imagen { get; set; } = string.Empty;

        public string Estado { get; set; } = "Pendiente";

        public string Prioridad { get; set; } = "Normal";

        public string Fecha { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}
