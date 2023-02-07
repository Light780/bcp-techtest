using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using BCP.Application.Interfaces.Common;
using Microsoft.AspNetCore.Http;

namespace BCP.WebApi.Services
{
    public class UsuarioSession : IUsuarioSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public string GetUsuarioSession()
        {
            return _httpContextAccessor
                .HttpContext
                .User?
                .Claims?
                .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?
                .Value;
        }
    }
}