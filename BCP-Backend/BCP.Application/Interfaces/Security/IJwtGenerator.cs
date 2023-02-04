using BCP.Domain.Entities;

namespace BCP.Application.Interfaces.Security
{
    public interface IJwtGenerator
    {
        string CreateToken(Usuario usuario);
    }
}