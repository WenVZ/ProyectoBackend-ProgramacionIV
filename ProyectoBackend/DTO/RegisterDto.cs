namespace ProyectoBackend.DTO
{
    //DTO (Data Transfer Object)
    //objeto que solo sirve para transportar datos entre capas de aplicacion
    public class RegisterDto
    {


        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
