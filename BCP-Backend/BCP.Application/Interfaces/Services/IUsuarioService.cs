using System.Threading.Tasks;
using BCP.Application.DTOs.Usuario;
using BCP.Application.Wrappers;

namespace BCP.Application.Interfaces.Services
{
    public interface IUsuarioService
    {
        public Task<Response<LoginUsuarioResponse>> Register(RegisterUsuarioRequest request);

        public Task<Response<LoginUsuarioResponse>> Login(LoginUsuarioRequest request);
        
        public Task<Response<LoginUsuarioResponse>> GetByToken();
    }
}