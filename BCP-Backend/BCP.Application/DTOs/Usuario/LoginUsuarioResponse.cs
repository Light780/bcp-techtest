namespace BCP.Application.DTOs.Usuario
{
    public class LoginUsuarioResponse
    {
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Token { get; set; }
    }
}