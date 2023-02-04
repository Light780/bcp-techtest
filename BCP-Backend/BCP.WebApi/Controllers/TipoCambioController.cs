using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCP.Application.DTOs.TipoCambio;
using BCP.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BCP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoCambioController : ControllerBase
    {
        private readonly ITipoCambioService _tipoCambioService;

        public TipoCambioController(ITipoCambioService tipoCambioService)
        {
            _tipoCambioService = tipoCambioService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetTipoCambioRequest request)
        {
            var response = await _tipoCambioService.Get(request);
            return Ok(response);
        }
        
        [HttpGet("convertAmount")]
        public async Task<IActionResult> ConvertAmount([FromQuery] ConvertTipoCambioRequest request)
        {
            var response = await _tipoCambioService.ConvertAmount(request);
            return Ok(response);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTipoCambioRequest request)
        {
            var response = await _tipoCambioService.Create(request);
            return Ok(response);
        }
        
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTipoCambioRequest request)
        {
            var response = await _tipoCambioService.Update(request);
            return Ok(response);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteTipoCambioRequest request)
        {
            var response = await _tipoCambioService.Delete(request);
            return Ok(response);
        }
    }
}