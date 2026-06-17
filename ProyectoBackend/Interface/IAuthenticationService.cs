namespace ProyectoBackend.Services
{
    public interface IAuthenticationService
    {
        Task<string?> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(string nombre, string correo, string password);

    }
}
