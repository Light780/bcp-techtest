using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCP.Application.Interfaces.Services.Moneda;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BCP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedaController : ControllerBase
    {
        private readonly IMonedaService _monedaService;

        public MonedaController(IMonedaService monedaService)
        {
            _monedaService = monedaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _monedaService.GetAll();
            return Ok(response);
        }
    }
}