using System.Threading.Tasks;
using BCP.Application.DTOs.Usuario;
using BCP.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BCP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUsuarioRequest request)
        {
            var response = await _usuarioService.Register(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioRequest request)
        {
            var response = await _usuarioService.Login(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetByToken()
        {
            var response = await _usuarioService.GetByToken();
            return Ok(response);
        } 
    }
}