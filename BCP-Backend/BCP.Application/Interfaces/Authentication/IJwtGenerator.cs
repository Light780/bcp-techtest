using BCP.Domain.Entities;

namespace BCP.Application.Interfaces.Authentication
{
    public interface IJwtGenerator
    {
        string GenerateToken(Usuario usuario);
    }
}