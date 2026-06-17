namespace ProyectoBackend.Entities
{
    public class Reserva
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Fecha { get; set; } = string.Empty;

        public string HoraEvento { get; set; } = string.Empty;

        public int Personas { get; set; }

        public string Actividad { get; set; } = string.Empty;

        public string Estado { get; set; } = "Pendiente";
    }
}
